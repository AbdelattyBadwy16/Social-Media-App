using SocialMedia.Models;

namespace SocialMedia.Repository
{
	public class PhotoRepository : IPhotoRepository
	{
		public PhotoRepository() { }
		AppDbContext _context = new AppDbContext();

		public List<Photo> GetTop3(string id)
		{
			var photos = _context.photos.Where(x => x.UserId == id).Take(3).ToList();
			return photos;
		}

		public List<Photo> Get(string id)
		{
			List<Photo> photos = _context.photos.Where(x => x.UserId == id).ToList(); 
			return photos; 
		}
	}
}
