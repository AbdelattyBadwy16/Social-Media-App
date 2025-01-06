using Microsoft.AspNetCore.Mvc;
using SocialMedia.Application.Repository;
using SocialMedia.Application.Response;
using SocialMedia.Core.Models;
using SocialMedia.Infrastructure.Models;


namespace SocialMedia.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class FavouritPostController : ControllerBase
	{
		private readonly AppDbContext _DB;
		private readonly IFavouritPostRepository favouritPostRepository;
		public FavouritPostController(AppDbContext DB , IFavouritPostRepository _favPostRepo)
		{
			_DB = DB;
			favouritPostRepository = _favPostRepo;
		}
	


		[HttpPost("addPost")]

		public async Task<Response<string>> AddPost(string userId, int PostId)
		{
			if (ModelState.IsValid)
			{
				FavouritPost favouritPost = new FavouritPost()
				{
					UserId = userId,
					PostId = PostId
				};
				return await favouritPostRepository.Add(favouritPost);
			}
			return Response<string>.Failure("Faild to add post");
		}


		[HttpDelete("DeletePost")]

		public async Task<Response<string>> DeletePost(string userId, int PostId)
		{
			if (ModelState.IsValid)
			{
				var post = await favouritPostRepository.Find(userId, PostId);
				if (post != null)
				{
					return await favouritPostRepository.Delete(post);
				}
			}
			return Response<string>.Failure("Faild to delete post");
		}


		[HttpGet]

		public async Task<Response<List<FavouritPost>>> GetUserFavPost(string userId)
		{
			if (ModelState.IsValid)
			{
				return await favouritPostRepository.GetAll(userId);
			}
			return Response<List<FavouritPost>>.Failure("Cant get data");
		}


		[HttpGet("Check")]

		public async Task<Response<string>> CheckFavPost(string userId,int PostId)
		{

			if (ModelState.IsValid)
			{
				var post = favouritPostRepository.Find(userId, PostId);
				if(post is not null)
				{
					return Response<string>.Success("Found");
				}
				return Response<string>.Success("Not Found");
			}
			return  Response<string>.Failure("faild to check.");
		}
	}
}
