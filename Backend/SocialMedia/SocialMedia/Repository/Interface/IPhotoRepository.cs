using SocialMedia.Models;

namespace SocialMedia.Repository
{
	public interface IPhotoRepository
	{
		Task<List<Photo>> GetTop3(string id);
		Task<List<Photo>>  Get(string id);
	}
}
