using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Application.Repository;
using SocialMedia.Infrastructure.Models;


namespace SocialMedia.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PhotoController : ControllerBase
	{
		private readonly IPhotoRepository photoRepository;
		public PhotoController(AppDbContext DB , IPhotoRepository _photoRepo)
		{
			photoRepository = _photoRepo;
		}

		[HttpGet]

		public async Task<IActionResult> GetPostByIdTop3(string id)
		{
			if (ModelState.IsValid)
			{
				var photos = await photoRepository.GetTop3(id);
				return Ok(photos);
			}
			return BadRequest();
		}


		[HttpGet("AllPhotos")]

		public async Task<IActionResult>  GetPostById(string id)
		{
			if (ModelState.IsValid)
			{
				var photos = await photoRepository.Get(id);
				return Ok(photos);
			}
			return BadRequest();
		}

	}
}
