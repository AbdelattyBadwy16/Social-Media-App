using SocialMedia.Application.Response;
using SocialMedia.Core.Models;

namespace SocialMedia.Application.Repository
{
	public interface IPhotoRepository
	{
		Task<Response<List<Photo>>> GetTop3(string id);
		Task<Response<List<Photo>>>  Get(string id);
	}
}
