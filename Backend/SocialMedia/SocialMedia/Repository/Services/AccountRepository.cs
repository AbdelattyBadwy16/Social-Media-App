using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Models;
using Microsoft.AspNetCore.Identity;

namespace SocialMedia.Repository
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
