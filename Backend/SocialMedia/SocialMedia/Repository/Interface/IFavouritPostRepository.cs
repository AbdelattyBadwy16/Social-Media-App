using SocialMedia.Data;

namespace SocialMedia.Repository
{
	public interface IFavouritPostRepository
	{
		Task<FavouritPost?> Find(string userId, int PostId);
		Task<List<FavouritPost>> GetAll(string userId);
		Task Add(FavouritPost favouritPost);
		Task Delete(FavouritPost favouritPost);
	}
}
