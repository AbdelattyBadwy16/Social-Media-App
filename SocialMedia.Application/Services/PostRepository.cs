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
using SocialMedia.Application.Response;


namespace SocialMedia.Application.Repository
{
	public class PostRepository : IPostRepository
	{
		public PostRepository() {}
		AppDbContext _context = new AppDbContext();
		public async Task<Response<int>> AddAsync(dtoPost NewPost)
		{
			Post post = NewPost.ToPost();
			try
			{
				await _context.posts.AddAsync(post);
				await _context.SaveChangesAsync();
			}catch(Exception ex)
			{
				return Response<int>.Failure("faild to add post");
			}
			return Response<int>.Success(post.Id,"",200);
		}

		public async Task<Post?> Find(int id)
		{
			return await _context.posts.FirstOrDefaultAsync(p=>p.Id == id);
		}                               

		public async Task<Response<string>> AddPostImage(IHostingEnvironment _host,IFormFile image,int id)
		{
			
			Post? post = await Find(id);
			
			if(post == null)return Response<string>.Failure("Post Not Found");
			
			string ImageName;
			//add post photo
			ImageName = AddPostPhoto(_host, image, "postPhotos");
			//add user photo
			ImageName = AddPostPhoto(_host, image, "userPhotos");
	
			post.ImagePath = ImageName;
			try{
				await _context.SaveChangesAsync();
			}catch(Exception ex)
			{
				return Response<string>.Failure("faild to add post image");
			}
			return Response<string>.Success("Image Post Added.");
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

		public async Task<Response<List<Post>>> GetAll()
		{
			return Response<List<Post>>.Success(await _context.posts.OrderByDescending(item => item.Id).ToListAsync());
		}

		public async Task<Response<List<Post>>> GetAllByUser(string userId)
		{
			
			return Response<List<Post>>.Success(await _context.posts.Where((item) => item.UserId == userId).OrderByDescending((item) => item.Id).ToListAsync());
		}

		public async Task<Response<string>> Delete(int id)
		{
			Post? post = await Find(id);
			if(post == null)
			{
				return Response<string>.Failure("Post Not Found.");
			}
			try
			{
				_context.posts.Remove(post);
				
				await _context.SaveChangesAsync();
			}catch(Exception ex)
			{	
				return Response<string>.Failure("Faild to delete post.");
			}
			return Response<string>.Success("Post Deleted.");
		}

		public async Task UpdateReact(int postid , string type , int payload)
		{
			Post? post = await Find(postid);
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

		public async Task<Response<string>> Update(string content , string status , int id)
		{
			Post? post = await Find(id);
			if (post == null)
			{
				return Response<string>.Failure("Post Not Found.");
			}
			try
			{
				post.Content = content;
				post.Status = status;
				await _context.SaveChangesAsync();
			}catch(Exception ex)
			{
				return Response<string>.Failure("Faild to update post.");
			}
			return Response<string>.Success("Post updated.");
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
