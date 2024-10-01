using Microsoft.EntityFrameworkCore;
using SocialMedia.Models;

namespace SocialMedia.Repository
{
	public class PhotoRepository : IPhotoRepository
	{
		public PhotoRepository() { }
		AppDbContext _context = new AppDbContext();

		public async Task<List<Photo>> GetTop3(string id)
		{
			var photos = new List<Photo>();
			try
			{
				photos = await _context.photos.Where(x => x.UserId == id).Take(3).ToListAsync();
			}catch(Exception ex)
			{
				throw new Exception(ex.Message);
			}
			return photos;
		}

		public async Task<List<Photo>> Get(string id)
		{
			List<Photo> photos = new List<Photo>();
			try
			{
			    photos = await _context.photos.Where(x => x.UserId == id).ToListAsync(); 
			}catch(Exception ex)
			{
				throw new Exception(ex.Message);
			}
			return photos; 
		}
	}
}
