using SocialMedia.Application.DTOs;
using SocialMedia.Core.Models;

namespace SocialMedia.Application.Repository
{
	public interface IAccountRepository
	{
        public Task<bool> CreateNewUser(dtoNewUser user);
	}
}
