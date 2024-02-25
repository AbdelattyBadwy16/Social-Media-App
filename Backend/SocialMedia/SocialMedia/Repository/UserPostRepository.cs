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

		public void Delete(User_Post temp)
		{
			_context.user_Posts.Remove(temp);
			_context.SaveChanges();
		}

		public void Add(User_Post userPost)
		{
			_context.user_Posts.Add(userPost);
			_context.SaveChanges();
		}
	}
}
