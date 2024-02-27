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
using SocialMedia.Repository;

namespace SocialMedia.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	
	public class PostController : ControllerBase
	{
		private readonly IHostingEnvironment _host;

		public PostController(AppDbContext DB,IHostingEnvironment host , IPostRepository _postRepo , IUserPostRepository _userPostRepo , IUserRepository _userRepo , ICommentRepository _commentRepo)
		{
			_host = host;
			postRepository = _postRepo;
			userPostRepository = _userPostRepo;
			userRepository = _userRepo;
			commentRepository = _commentRepo;
		}

		public IPostRepository postRepository ;
		public IUserPostRepository userPostRepository;
		public IUserRepository userRepository;
		public ICommentRepository commentRepository;
		[HttpPost]
		public async Task<IActionResult> AddNewPostAsync(dtoPost post)
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

				await postRepository.AddAsync(NewPost);

				return Ok(NewPost.Id);
			}

			return BadRequest(ModelState);

		}

		[HttpPut("PostImage")]
		public async Task<IActionResult> AddPostImage(IFormFile image,int id)
		{
			if (ModelState.IsValid)
			{
				string ImageName;
				//add post photo
				ImageName = postRepository.AddPostPhoto(_host, image, "postPhotos");
				//add user photo
				ImageName = postRepository.AddPostPhoto(_host, image, "userPhotos");
				
				Post? post = await postRepository.Find(id);
				if(post  != null)
				{
					postRepository.AddPostImage(post, ImageName);
				}
				return Ok(ModelState);
			}

			return BadRequest(ModelState);

		}

		[HttpGet("getPost")]
		public async Task<IActionResult> GetPost(int id)
		{
			var post = await postRepository.Find(id);
			if(post != null) {
			   return Ok(post);
			}
			return NotFound();

		}



		[HttpGet]

		public IActionResult GetAllPosts()
		{
			var posts = postRepository.GetAll();
			return Ok(posts);
		}


		[HttpGet("userPost")]

		public IActionResult GetAllPostsByUser(string userId)
		{
			List<Post> posts = postRepository.GetAllByUser(userId);
			return Ok(posts);
		}


		[HttpDelete]

		public async Task<IActionResult> DeletePost(int id)
		{
			Post? post = await postRepository.Find(id);
			if (post != null)
			{
				postRepository.Delete(post);
			}
			return Ok();
		}

		[HttpPut("AddPostReact")]
		public async Task<IActionResult> UpdateLikes(string type, int id , string userId)
		{
			Post? post = await postRepository.Find(id);
			User_Post? temp = await userPostRepository.Get(id, userId);

			if (temp != null)
			{
				userPostRepository.Delete(temp);
				if (post != null)
				{
					postRepository.UpdateReact(post, temp.type, -1);
				}
			}
			int postId = 0;
			if (post != null)
			{
				postRepository.UpdateReact(post, type, 1);
				postId = post.Id;
			}

			User_Post user_Post = new User_Post()
			{
				UserId = userId,
				PostId = postId,
				type = type,
			};
			
			userPostRepository.Add(user_Post);
			return Ok();
		}


		[HttpPut("EditPost")]
		public async Task<IActionResult> EditPost(string content , string status ,int id)
		{
			Post? post = await postRepository.Find(id);
			if (post != null)
			{
				postRepository.Update(content, status, post);
			}

			return Ok();
		}


		[HttpGet("CheckReact")]
		public async Task<IActionResult> CheckPostReact(string userId, int id)
		{
			User_Post? temp = await userPostRepository.Get(id,userId);
			if(temp != null)
			{
				return Ok(temp.type);
			}

			return Ok(0);
		}


		[HttpPut("RemoveReact")]
		public async Task<IActionResult> RemovePostReact(string userId, int id)
		{
			Post? post =await postRepository.Find(id); //await _context.posts.FindAsync(id);
			User_Post? temp = await userPostRepository.Get(id,userId);


			if (temp != null)
			{
				userPostRepository.Delete(temp);
			}
			if (post != null && temp != null)
			{
				postRepository.UpdateReact(post, temp.type, -1);
			}
			return Ok(0);
		}


		[HttpPost("AddComment")]
		public async Task<IActionResult> AddComment(dtoComment comment)
		{
		    User? user = await userRepository.Get(comment.UserId);
			string userName = "";
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

			commentRepository.Add(NewComment);
			return Ok(ModelState);
		}

		[HttpDelete("DeleteComment")]
		public IActionResult DeleteComment(int id)
		{
			Comment? comment = commentRepository.Find(id);
			if(comment != null)
			{
				commentRepository.Delete(comment);
				return Ok(ModelState);
			}
			return BadRequest(ModelState);
		}


		[HttpGet("GetPostComments")]
		public IActionResult GetPostComments(int Id)
		{
			List<Comment> Comments = commentRepository.FindByPost(Id);
			
			return Ok(Comments);
		}
	}

}
