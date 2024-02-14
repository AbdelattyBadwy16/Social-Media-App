using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Data;
using SocialMedia.Models;

namespace SocialMedia.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class FavouritPostController : ControllerBase
	{
		public FavouritPostController(AppDbContext DB)
		{
			_DB = DB;
		}
		private readonly AppDbContext _DB;



		[HttpPost("addPost")]

		public async Task<IActionResult> AddPost(string userId, int PostId)
		{
			FavouritPost favouritPost = new FavouritPost()
			{
				UserId = userId,
				PostId = PostId
			};
			await _DB.favouritPosts.AddAsync(favouritPost);
			await _DB.SaveChangesAsync();
			return Ok(ModelState);
		}


		[HttpDelete("DeletePost")]

		public async Task<IActionResult> DeletePost(string userId, int PostId)
		{

			var post = _DB.favouritPosts.FirstOrDefault(post=>post.UserId == userId && post.PostId==PostId);
			if (post != null)
			{
				_DB.favouritPosts.Remove(post);
				await _DB.SaveChangesAsync();
				return Ok(ModelState);
			}else return BadRequest(ModelState);
		}


		[HttpGet]

		public async Task<IActionResult> GetUserFavPost(string userId)
		{

			var post = _DB.favouritPosts.Where(post => post.UserId == userId).Include("post");
			return Ok(post);
		}


		[HttpGet("Check")]

		public async Task<IActionResult> CheckFavPost(string userId,int PostId)
		{

			var post = await _DB.favouritPosts.FirstOrDefaultAsync(post => post.UserId == userId && post.PostId == PostId);
			if(post != null)
			{
				return Ok("Found");
			}else return Ok("Not Found");
			
		}
	}
}
