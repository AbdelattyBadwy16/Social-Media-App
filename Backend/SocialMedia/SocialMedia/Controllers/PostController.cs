using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using SocialMedia.Data;
using SocialMedia.Model;
using SocialMedia.Models;
using static System.Net.Mime.MediaTypeNames;
using System.Runtime.InteropServices.JavaScript;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Migrations;

namespace SocialMedia.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	
	public class PostController : ControllerBase
	{
		private readonly AppDbContext _context;
		private readonly IHostingEnvironment _host;

		public PostController(AppDbContext DB,IHostingEnvironment host)
		{
			_context = DB;
			_host = host;
		}

		

		[HttpPost]
		public async Task<IActionResult> AddNewPost(dtoPost post)
		{
			if (ModelState.IsValid)
			{

				Post NewPost = new()
				{
					Content = post.Content,
					Status = post.Status,
					UserId = post.UserId,
					GroupId = post.GroupId,
				    likes = 0,
					Wow = 0,
					Angry = 0,
					loves = 0,
					Sads = 0,
					Haha = 0,
					CreatedAt = DateTime.Now
				};

				await _context.posts.AddAsync(NewPost);
				await _context.SaveChangesAsync();
				return Ok(NewPost.Id);
			}

			return BadRequest(ModelState);

		}

		[HttpPut("PostImage")]
		public async Task<IActionResult> AddPostImage(IFormFile image,int id)
		{
			if (ModelState.IsValid)
			{

				// add photo to postPhotos
				string myUpload = Path.Combine(_host.WebRootPath, "postPhotos");
				string ImageName = image.FileName;
				string fullPath = Path.Combine(myUpload, ImageName);
				await image.CopyToAsync(new FileStream(fullPath, FileMode.Create));

				// add photo to userPhotos
				myUpload = Path.Combine(_host.WebRootPath, "userPhotos");
				ImageName = image.FileName;
				fullPath = Path.Combine(myUpload, ImageName);
				await image.CopyToAsync(new FileStream(fullPath, FileMode.Create));

				var Post = await _context.posts.FindAsync(id);

				Post.ImagePath = ImageName;
				_context.SaveChanges();
				return Ok(ModelState);
			}

			return BadRequest(ModelState);

		}

		[HttpGet("getPost")]
		public async Task<IActionResult> GetPost(int id)
		{
			var post = await _context.posts.FindAsync(id);

			if(post != null) {
			   return Ok(post);
			}
			return NotFound();

		}



		[HttpGet]

		public async Task<IActionResult> GetAllPosts()
		{
			var posts = await _context.posts.OrderByDescending(item=>item.Id).ToListAsync();

			return Ok(posts);
		}


		[HttpGet("userPost")]

		public async Task<IActionResult> GetAllPostsByUser(string userId)
		{
			var posts = _context.posts.Where((item)=>item.UserId==userId).OrderByDescending((item)=>item.Id);

			return Ok(posts);
		}


		[HttpDelete]

		public async Task<IActionResult> DeletePost(int id)
		{
			Post post = await _context.posts.FindAsync(id);
			_context.posts.Remove(post);
			_context.SaveChanges();

			return Ok();
		}

		[HttpPut]
		public async Task<IActionResult> UpdateLikes(string type, int id , string userId)
		{
			Post post = await _context.posts.FindAsync(id);

			User_Post temp = _context.user_Posts.FirstOrDefault(x => x.PostId == post.Id && x.UserId == userId);

			if (temp != null)
			{
				_context.user_Posts.Remove(temp);

				if (temp.type == "like")
				{
					post.likes--;
				}
				else if (temp.type == "love")
				{
					post.loves--;
				}
				else if (temp.type == "angry")
				{
					post.Angry--;
				}
				else if (temp.type == "haha")
				{
					post.Haha--;
				}
				else if (temp.type == "sad")
				{
					post.Sads--;
				}
				else if (temp.type == "wow")
				{
					post.Wow--;
				}
			}
			

				if (type == "like")
				{
					post.likes++;
				}
				else if (type == "love")
				{
					post.loves++;
				}
				else if (type == "angry")
				{
					post.Angry++;
				}
				else if (type == "haha")
				{
					post.Haha++;
				}
				else if (type == "sad")
				{
					post.Sads++;
				}
				else if (type == "wow")
				{
					post.Wow++;
				}
			

			User_Post user_Post = new User_Post()
			{
				UserId = userId,
				PostId = post.Id,
				type = type,
			};

			
			
			_context.user_Posts.Add(user_Post);

			_context.SaveChanges();

			return Ok();
		}


		[HttpPut("EditPost")]
		public async Task<IActionResult> EditPost(string content , string status ,int id)
		{
			Post post = await _context.posts.FindAsync(id);
			post.Content = content;
			post.Status = status;
			_context.SaveChanges();

			return Ok();
		}


		[HttpGet("CheckReact")]
		public async Task<IActionResult> CheckPostReact(string userId, int id)
		{
			User_Post temp = _context.user_Posts.FirstOrDefault(x => x.PostId == id && x.UserId == userId);
			if(temp != null)
			{
				return Ok(temp.type);
			}

			return Ok(0);
		}


		[HttpPut("RemoveReact")]
		public async Task<IActionResult> RemovePostReact(string userId, int id)
		{
			Post post = await _context.posts.FindAsync(id);

			User_Post temp = _context.user_Posts.FirstOrDefault(x => x.PostId == id && x.UserId == userId);
			
			_context.user_Posts.Remove(temp);

			if (temp.type == "like")
			{
				post.likes--;
			}
			else if (temp.type == "love")
			{
				post.loves--;
			}
			else if (temp.type == "angry")
			{
				post.Angry--;
			}
			else if (temp.type == "haha")
			{
				post.Haha--;
			}
			else if (temp.type == "sad")
			{
				post.Sads--;
			}
			else if (temp.type == "wow")
			{
				post.Wow--;
			}
			_context.SaveChanges();
			return Ok(0);
		}


		[HttpPost("AddComment")]
		public async Task<IActionResult> AddComment(dtoComment comment)
		{
		    var user = _context.users.FirstOrDefault(user=>user.Id == comment.UserId);
			var userName = "";
			if (user != null)
			{
				userName = user.FirstName + " " + user.LastName;
			}
			else return BadRequest(ModelState);

			Comment NewComment = new Comment()
			{
				PostId = comment.PostId,
				Content = comment.Content,
				UserName = userName,
				UserId = comment.UserId,
				UserImagePath = user.IconImagePath
			};
			_context.Comments.Add(NewComment);
			_context.SaveChanges();
			return Ok(ModelState);
		}

		[HttpDelete("DeleteComment")]
		public async Task<IActionResult> DeleteComment(int id)
		{
			var user = await _context.Comments.FirstOrDefaultAsync(item => item.PostId == id); ;
			if(user != null)
			{
				_context.Comments.Remove(user);
				_context.SaveChanges();
				return Ok(ModelState);
			}
			return BadRequest(ModelState);
		}


		[HttpGet("GetPostComments")]
		public async Task<IActionResult> GetPostComments(int Id)
		{
			var Comments = _context.Comments.Where(comment => comment.PostId == Id);
			
			return Ok(Comments);
		}
	}

}
