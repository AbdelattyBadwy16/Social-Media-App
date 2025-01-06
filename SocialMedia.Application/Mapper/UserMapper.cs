using SocialMedia.Application.DTOs;
using SocialMedia.Core.Models;


namespace SocialMedia.Application.Mapper
{
    public static class UserMapper
    {
        public static User ToUser(this dtoNewUser dtoUser)
        {
        	return new User()
				{
					UserName = dtoUser.userName,
					Email = dtoUser.email,
					Country = dtoUser.Country,
					FirstName = dtoUser.FirstName,
					LastName = dtoUser.LastName,
					Gender = dtoUser.Gender,
					CreatedDate = DateTime.UtcNow,
					About = "I,m a new User in Glichat App.",
					IconImagePath = "profile.jpg",
					BackImagePath = "loginBack.jpg",
					JopTitle = "NewPie"
				};

        }
    }
}