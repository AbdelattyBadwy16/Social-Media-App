using SocialMedia.Models;

namespace SocialMedia.Repository
{
	public interface ICommentRepository
	{
		void Add(Comment comment);
		void Delete(Comment comment);
		Comment? Find(int id);
		List<Comment> FindByPost(int Id);
	}
}
