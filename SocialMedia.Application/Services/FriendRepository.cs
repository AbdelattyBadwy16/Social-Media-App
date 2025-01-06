using Microsoft.EntityFrameworkCore;
using SocialMedia.Application.Response;
using SocialMedia.Core.Models;
using SocialMedia.Infrastructure.Models;


namespace SocialMedia.Application.Repository
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
		public async Task<Response<string>> Add(string id, string followerId)
		{
			Friends friend = new Friends()
			{
					UserId = id,
					FollowerId = followerId
			};
			try
			{
				await _context.friends.AddAsync(friend);
				await UpdateFollower(id, followerId, 1);
				await _context.SaveChangesAsync();
			}catch(Exception ex)
			{
				return Response<string>.Failure("Faild to Add friend");
			}
			return Response<string>.Success("friend Added.");
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

		public async Task<Response<string>> Delete(string userId , string id)
		{
			Friends? friend = await Find(userId, id);
			if (friend == null)
			{
				return Response<string>.Failure("friend not found");
			} 
			_context.friends.Remove(friend);
			await UpdateFollower(userId, id, -1);
			await _context.SaveChangesAsync();
			return Response<string>.Success("friend Deleted.");
		}

		
		public async Task<Response<List<Friends>>> GetFollowing(string id)
		{
			return Response<List<Friends>>.Success(await _context.friends.Where((item) => item.UserId == id).ToListAsync(),"",200);
		}
		public async Task<Response<List<Friends>>> GetFollower(string id)
		{
			return Response<List<Friends>>.Success(await _context.friends.Where((item) => item.FollowerId == id).ToListAsync(),"",200);
		}

		public async Task<Response<bool>> Check(string userId,string id)
		{
			var user = _context.friends.Where((item) => item.UserId == userId).Select(item=>item.FollowerId == id);
			if(user is null) {
				return Response<bool>.Success(false);
			}
			return Response<bool>.Success(true);
		}
	}
}
