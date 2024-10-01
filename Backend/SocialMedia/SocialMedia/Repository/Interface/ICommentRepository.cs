using SocialMedia.Models;

namespace SocialMedia.Repository
{
	public interface ICommentRepository
	{
		Task Add(Comment comment);
		Task Delete(Comment comment);
		Task<Comment?> Find(int id);
		Task<List<Comment>> FindByPost(int Id);
	}
}
