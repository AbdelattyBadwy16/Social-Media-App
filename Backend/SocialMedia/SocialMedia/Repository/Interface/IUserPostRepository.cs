using SocialMedia.Data;

namespace SocialMedia.Repository
{
	public interface IUserPostRepository
	{
		Task<User_Post?> Get(int postId, string userId);
		Task Delete(User_Post temp);
		Task Add(User_Post userPost);
	}
}
