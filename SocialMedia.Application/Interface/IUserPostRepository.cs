using SocialMedia.Core.Models;

namespace SocialMedia.Application.Repository
{
	public interface IUserPostRepository
	{
		Task<User_Post?> Get(int postId, string userId);
		Task Delete(User_Post temp);
		Task Add(User_Post userPost);
	}
}
