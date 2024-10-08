﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SocialMedia.Data;
using SocialMedia.Mapper;
using SocialMedia.Model;
using SocialMedia.Models;
using SocialMedia.Repository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace SocialMedia.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AccountController : Controller
	{
		
		private readonly UserManager<User> _userManger;
		private readonly IConfiguration _configuration;
		private readonly AppDbContext _DB;
		private readonly IHostingEnvironment _host;

		private readonly IUserRepository _userRepository;
		public AccountController(UserManager<User> userManager, IConfiguration configuration , AppDbContext DB , IHostingEnvironment host , IUserRepository userRepository)
		{
			_userManger = userManager;
			_configuration = configuration;
			_host = host;
			_DB = DB;
			_userRepository = userRepository;
		}


		[HttpPost]
		public async Task<IActionResult> RegisterNewUser(dtoNewUser user)
		{

			if (ModelState.IsValid)
			{
				User newuser = user.ToUser();
				IdentityResult result = await _userManger.CreateAsync(newuser, user.password);
				if (result.Succeeded)
				{
					return Ok("Success");
				}
				else
				{
					foreach (var item in result.Errors)
					{
						ModelState.AddModelError("", item.Description);
					}
				}
			}
			return BadRequest(ModelState);
		}


		[HttpPost("Login")]

		public async Task<IActionResult> Login(dtoLogin login)
		{
			if (ModelState.IsValid)
			{
				var user = await _userManger.FindByNameAsync(login.username);

				if (user != null)
					if (await _userManger.CheckPasswordAsync(user, login.password))
					{
						var clamis = new List<Claim>();
						clamis.Add(new Claim(ClaimTypes.Name, user.UserName));
						clamis.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
						clamis.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
						var roles = await _userManger.GetRolesAsync(user);
						foreach (var role in roles)
						{
							clamis.Add(new Claim(ClaimTypes.Role, role.ToString()));
						}

						var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));
						var sc = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
						var mktoken = new JwtSecurityToken(
								claims: clamis,
								expires: DateTime.Now.AddMonths(1),
								signingCredentials: sc
							);
						var token = new
						{
							token = new JwtSecurityTokenHandler().WriteToken(mktoken),
						};

						return Ok(new { token.token, user.UserName, user.IconImagePath, user.Id });
					}
					else
					{
						return Unauthorized();
					}
			}
			else
			{
				ModelState.AddModelError("", "username is invalid");
			}
			return BadRequest(ModelState);

		}

		[HttpGet]
		[Authorize]

		public async Task<IActionResult> GetAccountInfo(string id)
		{
			if (ModelState.IsValid)
			{
				var user = await _userManger.FindByIdAsync(id);
				if (user is null)
				{
					return BadRequest(ModelState);
				}
				return Ok(user);
			}
			return BadRequest();
		}

		[HttpGet("about")]

		public async Task<IActionResult> UpdateAbout(string about, string id)
		{
			if (ModelState.IsValid)
			{
				var user = await _DB.users.FirstOrDefaultAsync(x=> x.Id == id);
				if (user == null)
				{
					return BadRequest(ModelState);
				}
				else
				{
					user.About = about;
					await _DB.SaveChangesAsync();
				}
				return Ok(user);
			}
			return BadRequest();

		}

		[HttpPut("IconImage")]

		public async Task<IActionResult> UpdateIconImage(IFormFile image, string id)
		{
			if (ModelState.IsValid)
			{
				var user = await _DB.users.FirstOrDefaultAsync(x => x.Id == id);
				
				if (user == null)
				{
					return BadRequest(ModelState);
				}
				else
				{
					// add photo to userBack
					string myUpload = Path.Combine(_host.WebRootPath, "userIcon");
					string ImageName = image.FileName;
					string fullPath = Path.Combine(myUpload, ImageName);
					await image.CopyToAsync(new FileStream(fullPath, FileMode.Create));
					user.IconImagePath = ImageName;

					// add photo to userPhotos
					myUpload = Path.Combine(_host.WebRootPath, "userPhotos");
					ImageName = image.FileName;
					fullPath = Path.Combine(myUpload, ImageName);
					await image.CopyToAsync(new FileStream(fullPath, FileMode.Create));
					user.IconImagePath = ImageName;

					Photo photo = new Photo()
					{
						UserId = user.Id,
						ImagePath = ImageName
					};

					await _DB.photos.AddAsync(photo);
					await _DB.SaveChangesAsync();
				}

				return Ok(ModelState);
			}
			return BadRequest();

		}


		[HttpPut("BackImage")]

		public async Task<IActionResult> UpdateBackImage(IFormFile image, string id)
		{
			if (ModelState.IsValid)
			{
				var user = await _DB.users.FirstOrDefaultAsync(x => x.Id == id);

				if (user == null)
				{
					return BadRequest(ModelState);
				}
				else
				{
					// add photo to userBack
					string myUpload = Path.Combine(_host.WebRootPath, "backIcon");
					string ImageName = image.FileName;
					string fullPath = Path.Combine(myUpload, ImageName);
					await image.CopyToAsync(new FileStream(fullPath, FileMode.Create));

					// add photo to userPhotos
					myUpload = Path.Combine(_host.WebRootPath, "userPhotos");
					ImageName = image.FileName;
					fullPath = Path.Combine(myUpload, ImageName);
					await image.CopyToAsync(new FileStream(fullPath, FileMode.Create));
					user.IconImagePath = ImageName;

					Photo photo = new Photo()
					{
						UserId = user.Id,
						ImagePath = ImageName
					};

					await _DB.photos.AddAsync(photo);

					await _DB.SaveChangesAsync();
				}

				return Ok(ModelState);
			}
			return BadRequest(ModelState);

		}


		[HttpGet("GetAllUser")]

		public async Task<IActionResult> GetAllUser()
		{
			if(ModelState.IsValid)
			{
				ICollection<User> users = await _userRepository.GetAll();
				return Ok(users);
			}
			return BadRequest();
		}

		[HttpPut("updateAccount")]
		public async Task<IActionResult> updateAccount(dtoEditUser user)
		{
			if(ModelState.IsValid)
			{
				var EditedUser = await _userManger.FindByIdAsync(user.Id);
				if (EditedUser != null)
				{
					if (await _userManger.CheckPasswordAsync(EditedUser, user.OldPassword))
					{
						EditedUser.FirstName = user.FirstName;
						EditedUser.LastName = user.LastName;
						EditedUser.Country = user.Country;
						EditedUser.JopTitle = user.JopTitle;
						EditedUser.NickName = user.NickName;
						EditedUser.BirthDate = user.BirthDate;
						EditedUser.PhoneNumber = user.PhoneNumber;
						await _userManger.ChangePasswordAsync(EditedUser, user.OldPassword, user.Password);
						_DB.SaveChanges();
					}
					else return Unauthorized("Wronge Old Password.");
				}
				else return BadRequest();

				return Ok(user);
			}
			return BadRequest();
		}




	}
}