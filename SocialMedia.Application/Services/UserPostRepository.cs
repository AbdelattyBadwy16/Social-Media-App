using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using SocialMedia.Core.Models;
using SocialMedia.Infrastructure.Models;

namespace SocialMedia.Application.Repository
{
	public class UserPostRepository : IUserPostRepository
	{
		private readonly AppDbContext _context ;
		private readonly IPostRepository _postRepository;
		public UserPostRepository(AppDbContext context , IPostRepository postRepository) 
		{ 
			_context = context;
			_postRepository = postRepository;
		}

		public async Task<User_Post?> Get(int postId, string userId)
		{
			return await _context.user_Posts.FirstOrDefaultAsync(x => x.PostId == postId && x.UserId == userId);
		}

		public async Task Delete(int user_post_id,string userId)
		{
			User_Post? temp = await Get(user_post_id, userId);
			if (temp == null) return;
			try
			{
				_context.user_Posts.Remove(temp);
				await _postRepository.UpdateReact(user_post_id, temp.type, -1);
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
