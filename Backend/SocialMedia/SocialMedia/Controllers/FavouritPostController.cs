using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Data;
using SocialMedia.Models;
using SocialMedia.Repository;

namespace SocialMedia.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class FavouritPostController : ControllerBase
	{
		public FavouritPostController(AppDbContext DB , IFavouritPostRepository _favPostRepo)
		{
			_DB = DB;
			favouritPostRepository = _favPostRepo;
		}
		private readonly AppDbContext _DB;
		private readonly IFavouritPostRepository favouritPostRepository;


		[HttpPost("addPost")]

		public IActionResult AddPost(string userId, int PostId)
		{
			FavouritPost favouritPost = new FavouritPost()
			{
				UserId = userId,
				PostId = PostId
			};
			favouritPostRepository.Add(favouritPost);
			return Ok(ModelState);
		}


		[HttpDelete("DeletePost")]

		public IActionResult DeletePost(string userId, int PostId)
		{

			var post = favouritPostRepository.Find(userId, PostId);
			if (post != null)
			{
				favouritPostRepository.Delete(post);
				return Ok(ModelState);
			}else return BadRequest(ModelState);
		}


		[HttpGet]

		public IActionResult GetUserFavPost(string userId)
		{
			var post = favouritPostRepository.GetAll(userId);
			return Ok(post);
		}


		[HttpGet("Check")]

		public IActionResult CheckFavPost(string userId,int PostId)
		{

			var post = favouritPostRepository.Find(userId, PostId);
			if(post != null)
			{
				return Ok("Found");
			}
			return Ok("Not Found");
		}
	}
}
