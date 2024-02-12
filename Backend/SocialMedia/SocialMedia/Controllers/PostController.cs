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

namespace SocialMedia.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	
	public class PostController : ControllerBase
	{

		public PostController(AppDbContext DB,IHostingEnvironment host)
		{
			_DB = DB;
			_host = host;
		}

		private readonly AppDbContext _DB;
		private readonly IHostingEnvironment _host;

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

				await _DB.posts.AddAsync(NewPost);
				await _DB.SaveChangesAsync();
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

				var Post = await _DB.posts.FindAsync(id);

				Post.ImagePath = ImageName;
				_DB.SaveChanges();
				return Ok(ModelState);
			}

			return BadRequest(ModelState);

		}

		[HttpGet("getPost")]
		public async Task<IActionResult> GetPost(int id)
		{
			Post post = await _DB.posts.FindAsync(id);

			return Ok(post);
		}



		[HttpGet]

		public async Task<IActionResult> GetAllPosts()
		{
			var posts = _DB.posts.OrderByDescending(item=>item.Id).ToList();

			return Ok(posts);
		}


		[HttpGet("userPost")]

		public async Task<IActionResult> GetAllPostsByUser(string userId)
		{
			var posts = _DB.posts.Where((item)=>item.UserId==userId).OrderByDescending((item)=>item.Id);

			return Ok(posts);
		}


		[HttpDelete]

		public async Task<IActionResult> DeletePost(int id)
		{
			Post post = await _DB.posts.FindAsync(id);
			_DB.posts.Remove(post);
			_DB.SaveChanges();

			return Ok();
		}

		[HttpPut]
		public async Task<IActionResult> UpdateLikes(string type, int id , string userId)
		{
			Post post = await _DB.posts.FindAsync(id);

			User_Post temp = _DB.user_Posts.FirstOrDefault(x => x.PostId == post.Id && x.UserId == userId);

			if (temp != null)
			{
				_DB.user_Posts.Remove(temp);

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

			
			
			_DB.user_Posts.Add(user_Post);

			_DB.SaveChanges();

			return Ok();
		}


		[HttpPut("EditPost")]
		public async Task<IActionResult> EditPost(string content , string status ,int id)
		{
			Post post = await _DB.posts.FindAsync(id);
			post.Content = content;
			post.Status = status;
			_DB.SaveChanges();

			return Ok();
		}


		[HttpGet("CheckReact")]
		public async Task<IActionResult> CheckPostReact(string userId, int id)
		{
			User_Post temp = _DB.user_Posts.FirstOrDefault(x => x.PostId == id && x.UserId == userId);
			if(temp != null)
			{
				return Ok(temp.type);
			}

			return Ok(0);
		}


		[HttpPut("RemoveReact")]
		public async Task<IActionResult> RemovePostReact(string userId, int id)
		{
			Post post = await _DB.posts.FindAsync(id);

			User_Post temp = _DB.user_Posts.FirstOrDefault(x => x.PostId == id && x.UserId == userId);
			
			_DB.user_Posts.Remove(temp);

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
			_DB.SaveChanges();
			return Ok(0);
		}


		[HttpPost("AddComment")]
		public async Task<IActionResult> AddComment(dtoComment comment)
		{
		    var user = _DB.users.FirstOrDefault(user=>user.Id == comment.UserId);
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
			_DB.Comments.Add(NewComment);
			_DB.SaveChanges();
			return Ok(ModelState);
		}


		[HttpGet("GetPostComments")]
		public async Task<IActionResult> GetPostComments(int Id)
		{
			var Comments = _DB.Comments.Where(comment => comment.PostId == Id);
			
			return Ok(Comments);
		}
	}

}
