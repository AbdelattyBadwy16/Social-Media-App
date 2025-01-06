using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SocialMedia.Application.DTOs;
using SocialMedia.Application.Mapper;
using SocialMedia.Application.Repository;
using SocialMedia.Application.Response;
using SocialMedia.Core.Models;
using SocialMedia.Infrastructure.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace SocialMedia.API.Controllers
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
		private readonly IAccountRepository _accountRepository;
		public AccountController(UserManager<User> userManager, IConfiguration configuration , AppDbContext DB , IHostingEnvironment host , IUserRepository userRepository,IAccountRepository accountRepository)
		{
			
			_userManger = userManager;
			_configuration = configuration;
			_host = host;
			_DB = DB;
			_userRepository = userRepository;
			_accountRepository =  accountRepository;
		}


		[HttpPost]
		public async Task<Response<string>> RegisterNewUser(dtoNewUser user)
		{
			if (ModelState.IsValid)
			{
				return await _accountRepository.CreateNewUser(user);
			}
			return Response<string>.Failure("Faild to create Account");
		}


		[HttpPost("Login")]

		public async Task<Response<dtoLoginResponse>> Login(dtoLogin login)
		{
			if (ModelState.IsValid)
			{
				return await _accountRepository.Login(login);
			}
			return Response<dtoLoginResponse>.Failure(new dtoLoginResponse(),"Faild to create Account",400);

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