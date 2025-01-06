using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Application.DTOs;
using SocialMedia.Application.Mapper;
using SocialMedia.Application.Response;
using SocialMedia.Core.Models;
using SocialMedia.Infrastructure.Models;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace SocialMedia.Application.Repository
{
	public class AccountRepository : IAccountRepository
	{
		private readonly IJWTService _jwtService;
		private readonly AppDbContext _context;

		private readonly UserManager<User> _userManger;

		public AccountRepository(IJWTService jWTService,UserManager<User>userManager,AppDbContext context) {
			_jwtService = jWTService;
			_userManger = userManager;
			_context = context;
		 }


		public async Task<User?> FindByName(string username)
		{
			return await _userManger.FindByNameAsync(username);
		}
		public async Task<Response<string>> CreateNewUser(dtoNewUser user)
		{
			User newuser = user.ToUser();
			IdentityResult result = await _userManger.CreateAsync(newuser, user.password);
			if(!result.Succeeded){ 
				return Response<string>.Failure("Faild to create Account");
			}
			return Response<string>.Success("Account Created.");
		}
		
		public async Task<Response<dtoLoginResponse>> Login(dtoLogin login)
		{
			var user = await _userManger.FindByNameAsync(login.username);

			if (user != null && await _userManger.CheckPasswordAsync(user, login.password)){
				
				var token = await  _jwtService.GenerateJwtToken(user);
				return Response<dtoLoginResponse>.Success(new dtoLoginResponse
				{
					Id = user.Id,
					username = user.UserName,
					IconImagePath = user.IconImagePath,
					token = new JwtSecurityTokenHandler().WriteToken(token),
					
				}, "User login successfully",200);						
				
			}
			
			return Response<dtoLoginResponse>.Failure(new dtoLoginResponse(), "Invalid Email or Password", 400);
		}
		
		public async Task<Response<User>> GetUser(string id)
		{
			return Response<User>.Success(await _userManger.FindByIdAsync(id));
		}
		
		public async Task<Response<User>> UpdateAbout(string about, string id)
		{
			var user = await _userManger.FindByIdAsync(id);
			if(user == null)
			{
				return Response<User>.Failure("Account Not Found");
			}
			try
			{
				user.About = about;
				await _context.SaveChangesAsync();
			}catch(Exception ex)
			{
				return Response<User>.Failure("Faild to update.");
			}
			return Response<User>.Success(user);
		}
		
		public async Task<Response<string>> UpdateProfileImage(IFormFile image, string id,IHostingEnvironment _host)
		{
			var user = await _context.users.FirstOrDefaultAsync(x => x.Id == id);
				
			if (user == null)
			{
				return Response<string>.Failure("User Not Found.");
			}
			await AddImage(image,user, _host, "userIcon");
			await AddImage(image,user, _host, "userPhotos");
			
			
			Photo photo = new Photo()
			{
				UserId = user.Id,
				ImagePath = user.IconImagePath
			};
			try
			{
				await _context.photos.AddAsync(photo);
				await _context.SaveChangesAsync();
			}catch(Exception ex)
			{
				return Response<string>.Failure("Faild to add photo.");
			}
			return Response<string>.Success("Profile Image Updated.");
		}
		
		public async Task<Response<string>> UpdateBackGroundImage(IFormFile image, string id,IHostingEnvironment _host)
		{
			var user = await _context.users.FirstOrDefaultAsync(x => x.Id == id);
				
			if (user == null)
			{
				return Response<string>.Failure("User Not Found.");
			}
			await AddImage(image,user, _host, "backIcon");
			await AddImage(image,user, _host, "userPhotos");
			
			
			Photo photo = new Photo()
			{
				UserId = user.Id,
				ImagePath = user.IconImagePath
			};
			try
			{
				await _context.photos.AddAsync(photo);
				await _context.SaveChangesAsync();
			}catch(Exception ex)
			{
				return Response<string>.Failure("Faild to add photo.");
			}
			return Response<string>.Success("Profile Image Updated.");
		}
		
		private async Task AddImage(IFormFile image,User user,IHostingEnvironment _host,string path)
		{
			string myUpload = Path.Combine(_host.WebRootPath, path);
			string ImageName = image.FileName;
			string fullPath = Path.Combine(myUpload, ImageName);
			await image.CopyToAsync(new FileStream(fullPath, FileMode.Create));
			user.IconImagePath = ImageName;
		}
		
		
		public async Task<Response<User>> UpdateAccount(dtoEditUser user)
		{
			var EditedUser = await _userManger.FindByIdAsync(user.Id);
			if(EditedUser == null)
			{
				return Response<User>.Failure("User not found.");
			}
			if (!await _userManger.CheckPasswordAsync(EditedUser, user.OldPassword))
			{
				return Response<User>.Failure("Wronge Old Password.");
			}
			EditedUser.FirstName = user.FirstName;
			EditedUser.LastName = user.LastName;
			EditedUser.Country = user.Country;
			EditedUser.JopTitle = user.JopTitle;
			EditedUser.NickName = user.NickName;
			EditedUser.BirthDate = user.BirthDate;
			EditedUser.PhoneNumber = user.PhoneNumber;
			await _userManger.ChangePasswordAsync(EditedUser, user.OldPassword, user.Password);
			_context.SaveChanges();
			
			return Response<User>.Success(EditedUser);
		}
	}
}
