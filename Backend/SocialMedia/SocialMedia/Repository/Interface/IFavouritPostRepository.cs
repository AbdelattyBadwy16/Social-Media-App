using SocialMedia.Data;

namespace SocialMedia.Repository
{
	public interface IFavouritPostRepository
	{
		FavouritPost? Find(string userId, int PostId);
		List<FavouritPost> GetAll(string userId);
		void Add(FavouritPost favouritPost);
		void Delete(FavouritPost favouritPost);
	}
}
