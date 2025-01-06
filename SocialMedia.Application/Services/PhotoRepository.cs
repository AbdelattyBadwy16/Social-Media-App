using Microsoft.EntityFrameworkCore;
using SocialMedia.Application.Response;
using SocialMedia.Core.Models;
using SocialMedia.Infrastructure.Models;

namespace SocialMedia.Application.Repository
{
	public class PhotoRepository : IPhotoRepository
	{
		public PhotoRepository() { }
		AppDbContext _context = new AppDbContext();

		public async Task<Response<List<Photo>>> GetTop3(string id)
		{
			return Response<List<Photo>>.Success(await _context.photos.Where(x => x.UserId == id).Take(3).ToListAsync(),"",200); 
		}

		public async Task<Response<List<Photo>>> Get(string id)
		{
			return Response<List<Photo>>.Success(await _context.photos.Where(x => x.UserId == id).ToListAsync(),"",200); 
		}
	}
}
