using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.Models;
using SocialMedia.Infrastructure.Models;


namespace SocialMedia.Application.Repository
{
	public class CommentRepository : ICommentRepository
	
	{
		public CommentRepository() { }
		AppDbContext _context = new AppDbContext();

		public async Task Add(Comment comment)
		{
			try
			{
				await _context.Comments.AddAsync(comment);
				await _context.SaveChangesAsync();
			}catch(Exception ex)
			{
				throw new Exception(ex.Message);
			}
			
		}

		public async Task Delete(Comment comment)
		{
			try
			{
				_context.Comments.Remove(comment);
				await _context.SaveChangesAsync();
			}catch(Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public Task<Comment?> Find(int id)
		{
			return _context.Comments.FirstOrDefaultAsync(item => item.PostId == id);
		}

		public Task<List<Comment>> FindByPost(int Id)
		{
			return _context.Comments.Where(comment => comment.PostId == Id).ToListAsync();
		}
	}
}
