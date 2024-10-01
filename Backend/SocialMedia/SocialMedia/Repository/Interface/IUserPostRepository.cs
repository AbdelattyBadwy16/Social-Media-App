using SocialMedia.Data;

namespace SocialMedia.Repository
{
	public interface IUserPostRepository
	{
		Task<User_Post?> Get(int postId, string userId);
		void Delete(User_Post temp);
		void Add(User_Post userPost);
	}
}
