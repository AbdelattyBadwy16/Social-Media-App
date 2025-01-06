using Microsoft.AspNetCore.Http;
using SocialMedia.Application.DTOs;
using SocialMedia.Core.Models;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace SocialMedia.Application.Repository
{
	public interface IPostRepository
	{
		Task<int> AddAsync(dtoPost NewPost);
		Task<Post?> Find(int id);
		void AddPostImage(Post post, string ImageName);
		string AddPostPhoto(IHostingEnvironment _host, IFormFile image, string path);
		Task<List<Post>> GetAll();
		Task<List<Post>> GetAllByUser(string userId);
		Task Delete(Post post);
		Task UpdateReact(Post post, string type, int payload);
		Task Update(string content, string status, Post post);
		Task<List<Post>> GetFollowingPost(string id);
	}
}
