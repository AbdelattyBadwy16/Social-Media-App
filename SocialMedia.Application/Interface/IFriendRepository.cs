using SocialMedia.Application.Response;
using SocialMedia.Core.Models;

namespace SocialMedia.Application.Repository
{
	public interface IFriendRepository
	{
		Task<Response<string>> Add(string id, string followerId);
		Task UpdateFollower(string id, string followerId, int payload);
		Task<Friends?> Find(string userId, string id);
		Task<Response<string>> Delete(string userId , string id);
		Task<Response<List<Friends>>> GetFollowing(string id);
		Task<Response<List<Friends>>> GetFollower(string id);
		Task<Response<bool>> Check(string userId, string id);
	}
}
