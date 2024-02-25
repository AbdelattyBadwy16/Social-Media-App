using Microsoft.EntityFrameworkCore;
using SocialMedia.Data;
using SocialMedia.Models;

namespace SocialMedia.Repository
{
	public class FriendRepository : IFriendRepository
	{
		public FriendRepository() { }
		AppDbContext _context = new AppDbContext();
		UserRepository userRepository = new UserRepository();
		public async void Add(Friends freind )
		{
			await _context.friends.AddAsync(freind);
			await _context.SaveChangesAsync();
		}

		public async void UpdateFollower(string id, string followerId , int payload)
		{
			User? user = await userRepository.Get(id);
			if (user != null)
			{
				user.Following += payload;
			}
			User? user2 = await userRepository.Get(followerId);
			if (user2 != null)
			{
				user2.Followers += payload;
			}
			userRepository.Save();
			
		}

		public async Task<Friends?> Find(string userId, string id)
		{
			return await _context.friends.FirstOrDefaultAsync((item) => item.UserId == userId && item.FollowerId == id);
		}

		public async void Delete(Friends? friend , string userId , string id)
		{
			if (friend != null)
			{
				_context.friends.Remove(friend);
			}
			await _context.SaveChangesAsync();

		}

		
		public List<Friends> GetFollowing(string id)
		{
			return _context.friends.Where((item) => item.UserId == id).ToList();
		}
		public List<Friends> GetFollower(string id)
		{
			return _context.friends.Where((item) => item.FollowerId == id).ToList();
		}

		public bool Check(string userId,string id)
		{
			var users = _context.friends.Where((item) => item.UserId == userId);
			bool res = false;
			foreach (var user in users)
			{
				if (user.FollowerId == id) res = true;
			}
			return res;
		}
	}
}
