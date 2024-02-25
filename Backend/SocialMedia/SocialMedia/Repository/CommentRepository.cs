using Microsoft.EntityFrameworkCore;
using SocialMedia.Migrations;
using SocialMedia.Models;

namespace SocialMedia.Repository
{
	public class CommentRepository : ICommentRepository
	{
		public CommentRepository() { }
		AppDbContext _context = new AppDbContext();

		public void Add(Comment comment)
		{
			_context.Comments.Add(comment);
			_context.SaveChanges();
		}

		public void Delete(Comment comment)
		{
			_context.Comments.Remove(comment);
			_context.SaveChanges();
		}

		public Comment? Find(int id)
		{
			return _context.Comments.FirstOrDefault(item => item.PostId == id);
		}

		public List<Comment> FindByPost(int Id)
		{
			return _context.Comments.Where(comment => comment.PostId == Id).ToList();
		}
	}
}
