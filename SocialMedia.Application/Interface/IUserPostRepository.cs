using SocialMedia.Core.Models;

namespace SocialMedia.Application.Repository
{
	public interface IUserPostRepository
	{
		Task<User_Post?> Get(int postId, string userId);
		Task Delete(int user_post_id,string userId);
		Task Add(User_Post userPost);
	}
}
