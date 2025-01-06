using SocialMedia.Application.Response;
using SocialMedia.Core.Models;

namespace SocialMedia.Application.Repository
{
	public interface IFavouritPostRepository
	{
		Task<FavouritPost?> Find(string userId, int PostId);
		Task<Response<List<FavouritPost>>> GetAll(string userId);
		Task<Response<string>> Add(FavouritPost favouritPost);
		Task<Response<string>> Delete(FavouritPost favouritPost);
	}
}
