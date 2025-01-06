using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.Models;
using SocialMedia.Infrastructure.Models;

namespace SocialMedia.Application.Repository
{
	public class UserRepository : IUserRepository
	{
		private readonly AppDbContext _context;
		public UserRepository(AppDbContext context) 
		{ 
			_context = context;
		}
		

		public async Task<User?> Get(string id)
		{
			return await _context.users.FirstOrDefaultAsync(user => user.Id == id);
		}

		public async Task Save()
		{
			await _context.SaveChangesAsync();
		}
		
		public async Task<List<User>> GetAll()
		{
			List<User> users = new List<User>();
			try
			{
				users = await _context.users.OrderByDescending((item) => item.Followers).ToListAsync();
				
			}catch(Exception ex)
			{
				throw new Exception(ex.Message);
			}
			return users;
		}
	}
}
