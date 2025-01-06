using SocialMedia.Application.DTOs;
using SocialMedia.Application.Response;
using SocialMedia.Core.Models;

namespace SocialMedia.Application.Repository
{
	public interface ICommentRepository
	{
		Task<Response<string>> Add(dtoComment comment);
		Task<Response<string>> Delete(int od);
		Task<Comment?> Find(int id);
		Task<Response<List<Comment>>> FindByPost(int Id);
	}
}
