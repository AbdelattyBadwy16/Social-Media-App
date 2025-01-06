using Microsoft.Extensions.Hosting;
using static System.Net.Mime.MediaTypeNames;
using System.Runtime.InteropServices.JavaScript;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using System;
using System.Net.NetworkInformation;
using SocialMedia.Application.DTOs;
using SocialMedia.Core.Models;
using Microsoft.AspNetCore.Http;
using SocialMedia.Application.Mapper;
using SocialMedia.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;


namespace SocialMedia.Application.Repository
{
	public class PostRepository : IPostRepository
	{
		public PostRepository() {}
		AppDbContext _context = new AppDbContext();
		public async Task<int> AddAsync(dtoPost NewPost)
		{
			Post post = NewPost.ToPost();
			try
			{
				await _context.posts.AddAsync(post);
				await _context.SaveChangesAsync();
			}catch(Exception ex)
			{
				throw new Exception(ex.Message);
			}
			return post.Id;

		}

		public async Task<Post?> Find(int id)
		{
			return await _context.posts.FirstOrDefaultAsync(p=>p.Id == id);
		}                               

		public async void AddPostImage(Post post , string ImageName)
		{
			post.ImagePath = ImageName;
			await _context.SaveChangesAsync();
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

		public async Task<List<Post>> GetAll()
		{
			List<Post> posts = new List<Post>(); 
			try
			{
				posts = await _context.posts.OrderByDescending(item => item.Id).ToListAsync();
			}catch(Exception ex)
			{
				throw new Exception(ex.Message);
			}
			return posts;
		}

		public async Task<List<Post>> GetAllByUser(string userId)
		{
			List<Post> posts = new List<Post>(); 
			try
			{
				posts = await _context.posts.Where((item) => item.UserId == userId).OrderByDescending((item) => item.Id).ToListAsync();
			}catch(Exception ex)
			{
				throw new Exception(ex.Message);
			}
			return posts;
		}

		public async Task Delete(Post post)
		{
			try
			{
				_context.posts.Remove(post);
				await _context.SaveChangesAsync();
			}catch(Exception ex)
			{	
				throw new Exception(ex.Message);
			}
		}

		public async Task UpdateReact(Post post , string type , int payload)
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
			try
			{
				await _context.SaveChangesAsync();
			}catch(Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task Update(string content , string status , Post post)
		{
			try
			{
				post.Content = content;
				post.Status = status;
				await _context.SaveChangesAsync();
			}catch(Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
		 
		public async Task<List<Post>> GetFollowingPost(string id)
		{
			List<Post> posts = new List<Post>();
			try
			{
				await _context.posts.Where(post => post.UserId == id).ToListAsync();
			}catch(Exception ex)
			{
				throw new Exception(ex.Message);
			}
			return posts;
		}
	}
	
	
}
