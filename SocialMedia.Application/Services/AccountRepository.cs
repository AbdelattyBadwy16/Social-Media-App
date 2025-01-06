using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Optern.Infrastructure.ExternalServices.JWTService;
using SocialMedia.Application.DTOs;
using SocialMedia.Application.Mapper;
using SocialMedia.Core.Models;
using SocialMedia.Infrastructure.Models;

namespace SocialMedia.Application.Repository
{
	public class AccountRepository : IAccountRepository
	{
		IJWTService _jwtService;
		public AccountRepository(IJWTService jWTService) {
			_jwtService = jWTService;
		 }
		private readonly UserManager<User> _userManger;


		public async Task<User?> FindByName(string username)
		{
			return await _userManger.FindByNameAsync(username);
		}
		public async Task<bool> CreateNewUser(dtoNewUser user)
		{
			User newuser = user.ToUser();
			IdentityResult result = await _userManger.CreateAsync(newuser, user.password);
			return result.Succeeded;
		}
		
		// public async Task<IActionResult> Login(dtoLogin login)
		// {
		// 	// var user = await _userManger.FindByNameAsync(login.username);

		// 	// 	if (user != null)
		// 	// 		if (await _userManger.CheckPasswordAsync(user, login.password))
		// 	// 		{
		// 	// 			var token =  _jwtService.GenerateJwtToken(user);
		// 	// 			// return new { token, user.UserName, user.IconImagePath, user.Id };
						
		// 	// 		}
		// 	// 		else
		// 	// 		{
		// 	// 			return Unauthorized();
		// 	// 		}
		// }
	}
}
