using SocialMedia.Models;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace SocialMedia.Repository
{
	public interface IPostRepository
	{
		Task AddAsync(Post NewPost);
		Task<Post?> Find(int id);
		void AddPostImage(Post post, string ImageName);
		string AddPostPhoto(IHostingEnvironment _host, IFormFile image, string path);
		List<Post> GetAll();
		List<Post> GetAllByUser(string userId);
		void Delete(Post post);
		void UpdateReact(Post post, string type, int payload);
		void Update(string content, string status, Post post);
	}
}
