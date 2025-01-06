using Microsoft.AspNetCore.Http;
using SocialMedia.Application.DTOs;
using SocialMedia.Application.Response;
using SocialMedia.Core.Models;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace SocialMedia.Application.Repository
{
	public interface IPostRepository
	{
		Task<Response<int>> AddAsync(dtoPost NewPost);
		Task<Post?> Find(int id);
		Task<Response<string>> AddPostImage(IHostingEnvironment _host,IFormFile image,int id);
		string AddPostPhoto(IHostingEnvironment _host, IFormFile image, string path);
		Task<Response<List<Post>>> GetAll();
		Task<Response<List<Post>>> GetAllByUser(string userId);
		Task<Response<string>> Delete(int id);
		Task UpdateReact(int postid, string type, int payload);
		Task<Response<string>> Update(string content, string status, int id);
		Task<List<Post>> GetFollowingPost(string id);
	}
}
