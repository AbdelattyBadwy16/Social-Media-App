using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Optern.Application.Interfaces.ICacheService;
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
		
		private readonly IHostingEnvironment _host;
		private readonly ICacheService _casheService;
		private readonly IUserRepository _userRepository;
		private readonly IAccountRepository _accountRepository;
		public AccountController(ICacheService casheService, IConfiguration configuration , AppDbContext DB , IHostingEnvironment host , IUserRepository userRepository,IAccountRepository accountRepository)
		{
			
			_host = host;
			_userRepository = userRepository;
			_accountRepository =  accountRepository;
			_casheService = casheService;
		}


		[HttpPost]
		public async Task<Response<string>> RegisterNewUser(dtoNewUser user)
		{
			if (ModelState.IsValid)
			{
				_casheService.RemoveData("users");
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
			return Response<dtoLoginResponse>.Failure("Faild to create Account");

		}

		[HttpGet]
		[Authorize]

		public async Task<Response<User>> GetAccountInfo(string id)
		{
			if (ModelState.IsValid)
			{
				return await _accountRepository.GetUser(id);
			}
			return Response<User>.Failure("Faild to Get User");
		}

		[HttpGet("about")]

		public async Task<Response<User>> UpdateAbout(string about, string id)
		{
			if (ModelState.IsValid)
			{
				return await _accountRepository.UpdateAbout(about, id);
			}
			return Response<User>.Failure("Faild to update");

		}

		[HttpPut("IconImage")]

		public async Task<Response<string>> UpdateIconImage(IFormFile image, string id)
		{
			if (ModelState.IsValid)
			{
				return await _accountRepository.UpdateProfileImage(image,id,_host);
			}
			return Response<string>.Failure("Faild to update profile image.");
		}


		[HttpPut("BackImage")]

		public async Task<Response<string>> UpdateBackImage(IFormFile image, string id)
		{
			if (ModelState.IsValid)
			{
				return await _accountRepository.UpdateBackGroundImage(image,id,_host);
			}
			return Response<string>.Failure("Faild to update background image.");

		}


		[HttpGet("GetAllUser")]

		public async Task<Response<List<User>>> GetAllUser()
		{
			if(ModelState.IsValid)
			{
				var users = _casheService.GetData<List<User>>("users");	
				
				if(users is not null)
				{
					return Response<List<User>>.Success(users);
				}					
				return await _userRepository.GetAll();
			}
			return Response<List<User>>.Failure("Faild to get users.");
		}

		[HttpPut("updateAccount")]
		public async Task<Response<User>> updateAccount(dtoEditUser user)
		{
			if(ModelState.IsValid)
			{
				return await _accountRepository.UpdateAccount(user);
			}
			return Response<User>.Failure("Faild to update user.");
		}




	}
}