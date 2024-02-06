using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Model;
using SocialMedia.Models;

namespace SocialMedia.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PhotoController : ControllerBase
	{
		public PhotoController(AppDbContext DB)
		{
			_DB = DB;
		}
		private readonly AppDbContext _DB;

		[HttpGet]

		public async Task<IActionResult> GetPostByIdTop3(string id)
		{
			var photos = _DB.photos.Where(x => x.UserId == id).Take(3);

			return Ok(photos);
		}


		[HttpGet("AllPhotos")]

		public async Task<IActionResult> GetPostById(string id)
		{
			var photos = _DB.photos.Where(x => x.UserId == id);

			return Ok(photos);
		}

	}
}
