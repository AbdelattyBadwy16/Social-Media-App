using SocialMedia.Application.Response;
using SocialMedia.Core.Models;

namespace SocialMedia.Application.Repository
{
	public interface IUserRepository
	{
		Task<User?> Get(string id);
		Task Save();
		Task<Response<List<User>>> GetAll();
	}
}
