using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SocialMedia.Model;
using SocialMedia.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SocialMedia.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AccountController : Controller
	{

		public AccountController(UserManager<User> userManager , IConfiguration configuration) 
		{
			_userManger  = userManager;
			_configuration = configuration;
		}

		private readonly UserManager<User> _userManger;
		private readonly IConfiguration _configuration;

		[HttpPost]
		public async Task<IActionResult> RegisterNewUser(dtoNewUser user)
		{

			if(ModelState.IsValid)
			{
				User newuser = new()
				{
					UserName = user.userName,
					Email = user.email,
					Country = user.Country,
					FirstName = user.FirstName,
					LastName = user.LastName,
					Gender = user.Gender,
					CreatedDate = DateTime.Now,
				};

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
						var roles =await _userManger.GetRolesAsync(user);
						foreach (var role in roles)
						{
							clamis.Add(new Claim(ClaimTypes.Role,role.ToString()));
						}

						var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));
						var sc  = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
						var mktoken = new JwtSecurityToken(
								claims: clamis,
								expires: DateTime.Now.AddMonths(1),
								signingCredentials : sc
							);
						var token = new
						{
							token = new JwtSecurityTokenHandler().WriteToken(mktoken),
						};

						return Ok(new {token.token,user.UserName,user.IconImage,user.Id});
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

		public async Task<IActionResult> GetAccountInfo(string UserName)
		{
			var user = await _userManger.FindByNameAsync(UserName);
			
			if(user ==  null)
			{
				return BadRequest(ModelState);
			}
				
			return Ok(user);

		}




	}
}
