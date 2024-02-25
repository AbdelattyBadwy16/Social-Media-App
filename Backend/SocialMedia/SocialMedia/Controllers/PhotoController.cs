using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Model;
using SocialMedia.Models;
using SocialMedia.Repository;

namespace SocialMedia.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PhotoController : ControllerBase
	{
		public PhotoController(AppDbContext DB)
		{
			photoRepository = new PhotoRepository();
		}
		private readonly PhotoRepository photoRepository;

		[HttpGet]

		public IActionResult GetPostByIdTop3(string id)
		{
			var photos = photoRepository.GetTop3(id);
			return Ok(photos);
		}


		[HttpGet("AllPhotos")]

		public IActionResult GetPostById(string id)
		{
			var photos = photoRepository.Get(id);
			return Ok(photos);
		}

	}
}
