using SocialMedia.Core.Models;

namespace SocialMedia.Application.Repository
{
	public interface IFriendRepository
	{
		Task Add(Friends freind);
		Task UpdateFollower(string id, string followerId, int payload);
		Task<Friends?> Find(string userId, string id);
		Task Delete(Friends? friend, string userId, string id);
		Task<List<Friends>> GetFollowing(string id);
		Task<List<Friends>> GetFollower(string id);
		Task<bool> Check(string userId, string id);
	}
}
