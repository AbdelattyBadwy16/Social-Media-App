using Microsoft.EntityFrameworkCore;
using SocialMedia.Data;
using SocialMedia.Models;

namespace SocialMedia.Repository
{
	public class FriendRepository : IFriendRepository
	{
		
		private readonly AppDbContext _context;
		private readonly IUserRepository userRepository;
		
		public FriendRepository(AppDbContext Context,IUserRepository UserRepository) 
		{
			_context = Context;
			userRepository = UserRepository;
		}
		public async Task Add(Friends freind )
		{
			try
			{
				await _context.friends.AddAsync(freind);
				await _context.SaveChangesAsync();
			}catch(Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task UpdateFollower(string id, string followerId , int payload)
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

		public async Task Delete(Friends? friend , string userId , string id)
		{
		
			_context.friends.Remove(friend);
			await _context.SaveChangesAsync();
		}

		
		public async Task<List<Friends>> GetFollowing(string id)
		{
			return await _context.friends.Where((item) => item.UserId == id).ToListAsync();
		}
		public async Task<List<Friends>> GetFollower(string id)
		{
			return await _context.friends.Where((item) => item.FollowerId == id).ToListAsync();
		}

		public async Task<bool> Check(string userId,string id)
		{
			var user = _context.friends.Where((item) => item.UserId == userId).Select(item=>item.FollowerId == id);
			if(user is null) return false;
			return true;
		}
	}
}
