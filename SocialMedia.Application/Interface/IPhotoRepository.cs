using SocialMedia.Core.Models;

namespace SocialMedia.Application.Repository
{
	public interface IPhotoRepository
	{
		Task<List<Photo>> GetTop3(string id);
		Task<List<Photo>>  Get(string id);
	}
}
