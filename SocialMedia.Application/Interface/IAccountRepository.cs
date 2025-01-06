using Microsoft.AspNetCore.Http;
using SocialMedia.Application.DTOs;
using SocialMedia.Application.Response;
using SocialMedia.Core.Models;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace SocialMedia.Application.Repository
{
	public interface IAccountRepository
	{
		public Task<Response<string>> CreateNewUser(dtoNewUser user);
		public Task<Response<dtoLoginResponse>> Login(dtoLogin login);
		public Task<Response<User>> GetUser(string id);
		public Task<Response<User>> UpdateAbout(string about, string id);
		public Task<Response<string>> UpdateProfileImage(IFormFile image, string id, IHostingEnvironment _host);
		public Task<Response<string>> UpdateBackGroundImage(IFormFile image, string id, IHostingEnvironment _host);
		public Task<Response<User>> UpdateAccount(dtoEditUser user);
	}
}
