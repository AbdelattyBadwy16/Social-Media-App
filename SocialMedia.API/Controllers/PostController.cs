using Microsoft.AspNetCore.Mvc;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using SocialMedia.Application.Repository;
using SocialMedia.Infrastructure.Models;
using SocialMedia.Application.DTOs;
using SocialMedia.Core.Models;
using SocialMedia.Application.Mapper;
using SocialMedia.Application.Response;

namespace SocialMedia.API.Controllers
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
		public async Task<Response<int>> AddNewPostAsync(dtoPost post)
		{
			if (ModelState.IsValid)
			{
				return await postRepository.AddAsync(post);
			}

			return Response<int>.Failure("faild to add post");

		}

		[HttpPut("PostImage")]
		public async Task<Response<string>> AddPostImage(IHostingEnvironment _host,IFormFile image,int id)
		{
			if (ModelState.IsValid)
			{
				return await postRepository.AddPostImage(_host,image,id);
			}

			return  Response<string>.Failure("faild to add photo");

		}

		[HttpGet("getPost")]
		public async Task<Response<Post>> GetPost(int id)
		{
			if (ModelState.IsValid)
			{
				var post = await postRepository.Find(id);
				if (post is null)
				{
					Response<Post>.Failure("Post Not Found");
				};
				return Response<Post>.Success(post);
			}
			return Response<Post>.Failure("Faild to add post");
		}



		[HttpGet]

		public async Task<Response<List<Post>>> GetAllPosts()
		{
			if (ModelState.IsValid)
			{
				return await postRepository.GetAll();
			}
			return Response<List<Post>>.Failure("Faild to get posts");
		}


		[HttpGet("userPost")]

		public async Task<Response<List<Post>>> GetAllPostsByUser(string userId)
		{
			if (ModelState.IsValid)
			{
				return await postRepository.GetAllByUser(userId);
			}
			return Response<List<Post>>.Failure("Faild to get posts");
		}


		[HttpDelete]

		public async Task<Response<string>> DeletePost(int id)
		{
			if (ModelState.IsValid)
			{
				return await postRepository.Delete(id);			
			}
			return Response<string>.Failure("Faild to delete post.");
		}

		[HttpPut("AddPostReact")]
		public async Task<Response<string>> UpdateLikes(string type, int id , string userId)
		{
			if (ModelState.IsValid)
			{					
				await userPostRepository.Delete(id,userId);
			
				// add new react
				await postRepository.UpdateReact(id, type, 1);
				
				User_Post user_Post = new User_Post()
				{
					UserId = userId,
					PostId = id,
					type = type,
				};

				await userPostRepository.Add(user_Post);
				return Response<string>.Success("React Updates.");
			}
			return Response<string>.Failure("Faild to update reacts.");
		}


		[HttpPut("EditPost")]
		public async Task<Response<string>> EditPost(string content , string status ,int id)
		{
			if (ModelState.IsValid)
			{
				return await postRepository.Update(content, status, id);
			}
			return Response<string>.Failure("Faild to update reacts.");
		}


		[HttpGet("CheckReact")]
		public async Task<Response<string>> CheckPostReact(string userId, int id)
		{
			if (ModelState.IsValid)
			{
				User_Post? temp = await userPostRepository.Get(id, userId);
				if (temp != null)
				{
					return Response<string>.Success(temp.type);
				}

				return Response<string>.Failure("Not Found");
			}
			return Response<string>.Failure("Faild to Check reacts.");
		}


		[HttpPut("RemoveReact")]
		public async Task<Response<string>> RemovePostReact(string userId, int id)
		{
			if (ModelState.IsValid)
			{
				await userPostRepository.Delete(id,userId);
				return Response<string>.Failure("react removed.");	
			}
			return Response<string>.Failure("Faild to remove reacts.");
		}


		[HttpPost("AddComment")]
		public async Task<Response<string>> AddComment(dtoComment comment)
		{
			if (ModelState.IsValid)
			{
				return await commentRepository.Add(comment);
			}
			return Response<string>.Failure("Faild to add comment.");
		}

		[HttpDelete("DeleteComment")]
		public async Task<Response<string>> DeleteComment(int id)
		{
			if(ModelState.IsValid)
			{
				return await commentRepository.Delete(id);
			}
			return Response<string>.Failure("Faild to remove comment.");
		}


		[HttpGet("GetPostComments")]
		public async Task<Response<List<Comment>>> GetPostComments(int Id)
		{
			if (ModelState.IsValid)
			{
				return await commentRepository.FindByPost(Id);
			}
			return Response<List<Comment>>.Failure("Faild to get comments.");
		}


	}

}
