using SocialMedia.Data;

namespace SocialMedia.Repository
{
	public interface IFriendRepository
	{
		void Add(Friends freind);
		void UpdateFollower(string id, string followerId, int payload);
		Task<Friends?> Find(string userId, string id);
		void Delete(Friends? friend, string userId, string id);
		List<Friends> GetFollowing(string id);
		List<Friends> GetFollower(string id);
		bool Check(string userId, string id);
	}
}
