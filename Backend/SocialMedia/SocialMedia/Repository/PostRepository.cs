using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using SocialMedia.Models;
using static System.Net.Mime.MediaTypeNames;
using System.Runtime.InteropServices.JavaScript;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using System;
using SocialMedia.Data;
using System.Net.NetworkInformation;

namespace SocialMedia.Repository
{
	public class PostRepository : IPostRepository
	{
		public PostRepository() {}
		AppDbContext _context = new AppDbContext();
		public async Task AddAsync(Post NewPost)
		{
			await _context.posts.AddAsync(NewPost);
			await _context.SaveChangesAsync();

		}

		public async Task<Post?> Find(int id)
		{
			return await _context.posts.FindAsync(id);
		}

		public void AddPostImage(Post post , string ImageName)
		{
			post.ImagePath = ImageName;
			_context.SaveChanges();
		}
		public string AddPostPhoto(IHostingEnvironment _host, IFormFile image , string path)
		{
			// add photo to postPhotos
			string myUpload = Path.Combine(_host.WebRootPath, path);
			string ImageName = image.FileName;
			string fullPath = Path.Combine(myUpload, ImageName);
			image.CopyTo(new FileStream(fullPath, FileMode.Create));
			return ImageName;
		}

		public List<Post> GetAll()
		{
			List<Post> posts = _context.posts.OrderByDescending(item => item.Id).ToList();
			return posts;
		}

		public List<Post> GetAllByUser(string userId)
		{
			List<Post> posts = _context.posts.Where((item) => item.UserId == userId).OrderByDescending((item) => item.Id).ToList();
			return posts;
		}

		public void Delete(Post post)
		{
			_context.posts.Remove(post);
			_context.SaveChanges();
		}

		public void UpdateReact(Post post , string type , int payload)
		{
			if (type == "like")
			{
				post.likes += payload;
			}
			else if (type == "love")
			{
				post.loves += payload;
			}
			else if (type == "angry")
			{
				post.Angry += payload;
			}
			else if (type == "haha")
			{
				post.Haha += payload;
			}
			else if (type == "sad")
			{
				post.Sads += payload;
			}
			else if (type == "wow")
			{
				post.Wow += payload;
			}
			_context.SaveChanges();
		}

		public void Update(string content , string status , Post post)
		{
			post.Content = content;
			post.Status = status;
			_context.SaveChanges();
		}
		 
		public List<Post> GetFollowingPost(string id)
		{
			List<Post> posts = _context.posts.Where(post=>post.UserId == id).ToList();
			return posts;
		}
	}
	
	
}
