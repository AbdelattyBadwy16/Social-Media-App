using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using SocialMedia.Data;
using SocialMedia.Models;

namespace SocialMedia.Repository
{
	public class UserPostRepository : IUserPostRepository
	{
		public UserPostRepository() { }
		AppDbContext _context = new AppDbContext();

		public async Task<User_Post?> Get(int postId, string userId)
		{
			return await _context.user_Posts.FirstOrDefaultAsync(x => x.PostId == postId && x.UserId == userId);
		}

		public async Task Delete(User_Post temp)
		{
			try
			{
				_context.user_Posts.Remove(temp);
				await _context.SaveChangesAsync();
			}catch(Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task Add(User_Post userPost)
		{
			try
			{
				await _context.user_Posts.AddAsync(userPost);
				await _context.SaveChangesAsync();
			}catch(Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
	}
}
