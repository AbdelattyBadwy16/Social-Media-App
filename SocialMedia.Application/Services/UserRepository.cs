using Microsoft.EntityFrameworkCore;
using Optern.Application.Interfaces.ICacheService;
using SocialMedia.Application.Response;
using SocialMedia.Core.Models;
using SocialMedia.Infrastructure.Models;

namespace SocialMedia.Application.Repository
{
	public class UserRepository : IUserRepository
	{
		private readonly AppDbContext _context;
		private readonly ICacheService _cacheService;
		public UserRepository(AppDbContext context , ICacheService cacheService) 
		{ 
			_context = context;
			_cacheService = cacheService;
		}
		

		public async Task<User?> Get(string id)
		{
			return await _context.users.FirstOrDefaultAsync(user => user.Id == id);
		}

		public async Task Save()
		{
			await _context.SaveChangesAsync();
		}
		
		public async Task<Response<List<User>>> GetAll()
		{
			var users = await _context.users.OrderByDescending((item) => item.Followers).ToListAsync();
			_cacheService.SetData("users", users);
			return Response<List<User>>.Success(users);
		}
	}
}
