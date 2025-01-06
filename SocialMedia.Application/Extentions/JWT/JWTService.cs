using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SocialMedia.Core.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Optern.Infrastructure.ExternalServices.JWTService
{
    public class JWTService 
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public JWTService(UserManager<User> _userManager, RoleManager<IdentityRole> _roleManager, IConfiguration _configuration)
        {
            this._userManager = _userManager;
            this._roleManager = _roleManager;
            this._configuration = _configuration;
        }
        public async Task<JwtSecurityToken> GenerateJwtToken(User User)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, User.UserName),
                new Claim(ClaimTypes.NameIdentifier, User.Id),
                new Claim(JwtRegisteredClaimNames.Sub, User.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email,User.Email),
                new Claim("uid", User.Id)
            };
            var roles = await _userManager.GetRolesAsync(User);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            SecurityKey Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));
            SigningCredentials signingCred = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);
            var Token = new JwtSecurityToken(
                issuer: _configuration["JWT:issuer"],
                audience: _configuration["JWT:audience"],
                claims: claims,
                signingCredentials: signingCred,
                expires: DateTime.UtcNow.AddDays(1)
                );
            return Token;
        }

    }
}