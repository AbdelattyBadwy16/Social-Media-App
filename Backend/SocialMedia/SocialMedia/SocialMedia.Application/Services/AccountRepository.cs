using Microsoft.AspNetCore.Identity;
using SocialMedia.Core.Models;
using SocialMedia.Infrastructure.Models;

namespace SocialMedia.Application.Repository
{
	public class AccountRepository
	{
		public AccountRepository() { }
		AppDbContext _context = new AppDbContext();
		private readonly UserManager<User> _userManger;


		public async Task<User?> FindByName(string username)
		{
			return await _userManger.FindByNameAsync(username);
		}

	}
}
