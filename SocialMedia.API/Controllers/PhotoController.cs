using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Application.Repository;
using SocialMedia.Application.Response;
using SocialMedia.Core.Models;
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

		public async Task<Response<List<Photo>>> GetPostByIdTop3(string id)
		{
			if (ModelState.IsValid)
			{
				return await photoRepository.GetTop3(id);
			}
			return Response<List<Photo>>.Failure("faild to get data");
		}


		[HttpGet("AllPhotos")]

		public async Task<Response<List<Photo>>>  GetPostById(string id)
		{
			if (ModelState.IsValid)
			{
				return await photoRepository.Get(id);
			}
			return Response<List<Photo>>.Failure("faild to get data");
		}

	}
}
