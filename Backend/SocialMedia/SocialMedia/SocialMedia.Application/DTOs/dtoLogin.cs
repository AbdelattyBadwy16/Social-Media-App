using System.ComponentModel.DataAnnotations;

namespace SocialMedia.Application.DTOs
{
	public class dtoLogin
	{
		[Required]
		public string username { get; set; }
		[Required]
		public string password { get; set; }
	}
}
