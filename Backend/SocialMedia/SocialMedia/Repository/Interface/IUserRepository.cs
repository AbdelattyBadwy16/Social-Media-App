using SocialMedia.Models;

namespace SocialMedia.Repository
{
	public interface IUserRepository
	{
		Task<User?> Get(string id);
		Task Save();
		Task<List<User>> GetAll();
	}
}
