using SocialMedia.Application.DTOs;
using SocialMedia.Core.Models;


namespace SocialMedia.Application.Mapper
{
	public static class PostMappers
	{
		public static Post ToPost(this dtoPost dtopost)
		{
			 return new Post
				{
					Content = dtopost.Content,
					Status = dtopost.Status,
					UserId = dtopost.UserId,
					GroupId = dtopost.GroupId,
					likes = 0,
					Wow = 0,
					Angry = 0,
					loves = 0,
					Sads = 0,
					Haha = 0,
					CreatedAt = DateTime.UtcNow
				};
		}
		
	}
}