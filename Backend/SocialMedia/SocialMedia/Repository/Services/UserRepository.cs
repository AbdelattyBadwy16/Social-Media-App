using Microsoft.EntityFrameworkCore;
using SocialMedia.Models;

namespace SocialMedia.Repository
{
	public class UserRepository : IUserRepository
	{
		public UserRepository() { }
		AppDbContext _context = new AppDbContext();

		public async Task<User?> Get(string id)
		{
			return await _context.users.FirstOrDefaultAsync(user => user.Id == id);
		}

		public void Save()
		{
			_context.SaveChanges();
		}
	}
}
