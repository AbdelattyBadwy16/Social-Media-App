using System.IdentityModel.Tokens.Jwt;
using SocialMedia.Core.Models;

namespace SocialMedia.Application.Repository
{
    public interface IJWTService
    {
        public Task<JwtSecurityToken> GenerateJwtToken(User User);
    }
}