using SocialMedia.Models;

namespace SocialMedia.Repository
{
	public interface IPhotoRepository
	{
		List<Photo> GetTop3(string id);
		List<Photo> Get(string id);
	}
}
