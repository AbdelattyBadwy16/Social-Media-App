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
using SocialMedia.Mapper;

namespace SocialMedia.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	
	public class PostController : ControllerBase
	{
		private readonly IHostingEnvironment _host;
		private readonly IPostRepository postRepository ;
		private readonly IUserPostRepository userPostRepository;
		private readonly IUserRepository userRepository;
		private readonly ICommentRepository commentRepository;
		private readonly IFriendRepository friendRepository;

		public PostController(AppDbContext DB,IHostingEnvironment host ,IFriendRepository _friendRepo, IPostRepository _postRepo , IUserPostRepository _userPostRepo , IUserRepository _userRepo , ICommentRepository _commentRepo)
		{
			_host = host;
			postRepository = _postRepo;
			userPostRepository = _userPostRepo;
			userRepository = _userRepo;
			commentRepository = _commentRepo;
			friendRepository = _friendRepo;
		}

		
		[HttpPost]
		public async Task<IActionResult> AddNewPostAsync(dtoPost post)
		{
			if (ModelState.IsValid)
			{
				var postId = await postRepository.AddAsync(post);
				return Ok(postId);
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
			if (ModelState.IsValid)
			{
				var post = await postRepository.Find(id);
				if (post is null) return NotFound();
				return Ok(post);
			}
			return BadRequest(ModelState);

		}



		[HttpGet]

		public async Task<IActionResult> GetAllPosts()
		{
			if (ModelState.IsValid)
			{
				var posts = await postRepository.GetAll();
				return Ok(posts);
			}
			return BadRequest(ModelState);
		}


		[HttpGet("userPost")]

		public async Task<IActionResult> GetAllPostsByUser(string userId)
		{
			if (ModelState.IsValid)
			{
				List<Post> posts = await postRepository.GetAllByUser(userId);
				return Ok(posts);
			}
			return BadRequest(ModelState);
		}


		[HttpDelete]

		public async Task<IActionResult> DeletePost(int id)
		{
			if (ModelState.IsValid)
			{

				Post? post = await postRepository.Find(id);
				if (post != null)
				{
					postRepository.Delete(post);
					return Ok();
				}
				return NotFound();
				
			}
			return BadRequest(ModelState);
		}

		[HttpPut("AddPostReact")]
		public async Task<IActionResult> UpdateLikes(string type, int id , string userId)
		{
			if (ModelState.IsValid)
			{
				Post? post = await postRepository.Find(id);
				User_Post? temp = await userPostRepository.Get(id, userId);
				// post not fount
				if (post is null) return NotFound();
				
				// user add react to this post before
				if (temp != null)
				{
					userPostRepository.Delete(temp);
					await postRepository.UpdateReact(post, temp.type, -1);
				}

				// add new react
				await postRepository.UpdateReact(post, type, 1);
				
				User_Post user_Post = new User_Post()
				{
					UserId = userId,
					PostId = post.Id,
					type = type,
				};

				userPostRepository.Add(user_Post);
				return Ok();
			}
			return BadRequest(ModelState);
		}


		[HttpPut("EditPost")]
		public async Task<IActionResult> EditPost(string content , string status ,int id)
		{
			if (ModelState.IsValid)
			{
				Post? post = await postRepository.Find(id);
				if (post != null)
				{
					await postRepository.Update(content, status, post);
					return Ok();
				}
				return NotFound();
				
			}
			return BadRequest(ModelState);
		}


		[HttpGet("CheckReact")]
		public async Task<IActionResult> CheckPostReact(string userId, int id)
		{
			if (ModelState.IsValid)
			{
				User_Post? temp = await userPostRepository.Get(id, userId);
				if (temp != null)
				{
					return Ok(temp.type);
				}

				return Ok(0);
			}
			return BadRequest(ModelState);
		}


		[HttpPut("RemoveReact")]
		public async Task<IActionResult> RemovePostReact(string userId, int id)
		{
			if (ModelState.IsValid)
			{
				Post? post = await postRepository.Find(id); //await _context.posts.FindAsync(id);
				User_Post? temp = await userPostRepository.Get(id, userId);
				if (post is null) return NotFound();
				if (temp != null)
				{
					userPostRepository.Delete(temp);
					await postRepository.UpdateReact(post, temp.type, -1);

				}
				return Ok(0);
			}
			return BadRequest(ModelState);
		}


		[HttpPost("AddComment")]
		public async Task<IActionResult> AddComment(dtoComment comment)
		{
			if (ModelState.IsValid)
			{
				User? user = await userRepository.Get(comment.UserId);
				if (user is null) return BadRequest("User Not Found");
	
				string userName = user.FirstName + " " + user.LastName;

				Comment NewComment = comment.ToComment();
				NewComment.UserImagePath = user.IconImagePath;
				NewComment.UserName = userName;
				
				commentRepository.Add(NewComment);
				return Ok(ModelState);
			}
			return BadRequest();
		}

		[HttpDelete("DeleteComment")]
		public async Task<IActionResult> DeleteComment(int id)
		{
			Comment? comment = await commentRepository.Find(id);
			if(comment != null)
			{
				await commentRepository.Delete(comment);
				return Ok(ModelState);
			}
			return BadRequest(ModelState);
		}


		[HttpGet("GetPostComments")]
		public async Task<IActionResult> GetPostComments(int Id)
		{
			if (ModelState.IsValid)
			{
				List<Comment> Comments = await commentRepository.FindByPost(Id);
				return Ok(Comments);
			}
			return BadRequest(ModelState);
		}

		[HttpGet("GetFollowingPost")]

		public async Task<IActionResult> GetFollowingPost(string UserId)
		{
			if (ModelState.IsValid)
			{
				List<Friends> friends = await friendRepository.GetFollowing(UserId);
				List<Post> posts = new List<Post>();
				foreach (Friends friend in friends)
				{
					List<Post> posts2 = await postRepository.GetFollowingPost(friend.FollowerId);
					foreach (Post post in posts2)
					{
						posts.Add(post);
					}
				}
				return Ok(posts);
			}
			return BadRequest(ModelState);
		}
	}

}
