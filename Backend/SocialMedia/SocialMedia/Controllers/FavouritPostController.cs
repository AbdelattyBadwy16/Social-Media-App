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
		private readonly AppDbContext _DB;
		private readonly IFavouritPostRepository favouritPostRepository;
		public FavouritPostController(AppDbContext DB , IFavouritPostRepository _favPostRepo)
		{
			_DB = DB;
			favouritPostRepository = _favPostRepo;
		}
	


		[HttpPost("addPost")]

		public async Task<IActionResult> AddPost(string userId, int PostId)
		{
			if (ModelState.IsValid)
			{
				FavouritPost favouritPost = new FavouritPost()
				{
					UserId = userId,
					PostId = PostId
				};
				await favouritPostRepository.Add(favouritPost);
				return Ok(ModelState);
			}
			return BadRequest(ModelState);
		}


		[HttpDelete("DeletePost")]

		public async Task<IActionResult> DeletePost(string userId, int PostId)
		{
			if (ModelState.IsValid)
			{
				var post = await favouritPostRepository.Find(userId, PostId);
				if (post != null)
				{
					await favouritPostRepository.Delete(post);
					return Ok(ModelState);
				}
				else return BadRequest(ModelState);
			}
			return BadRequest(ModelState);
		}


		[HttpGet]

		public async Task<IActionResult> GetUserFavPost(string userId)
		{
			if (ModelState.IsValid)
			{
				var post = await favouritPostRepository.GetAll(userId);
				return Ok(post);
			}
			return BadRequest(ModelState);
		}


		[HttpGet("Check")]

		public IActionResult CheckFavPost(string userId,int PostId)
		{

			if (ModelState.IsValid)
			{
				var post = favouritPostRepository.Find(userId, PostId);
				if(post is not null)
				{
					return Ok("Found");
				}
				return Ok("Not Found");
			}
			return BadRequest(ModelState);
		}
	}
}
