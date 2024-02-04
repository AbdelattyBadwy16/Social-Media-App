using System.ComponentModel.DataAnnotations;

namespace SocialMedia.Model
{
	public class dtoLogin
	{
		[Required]
		public string username { get; set; }
		[Required]
		public string password { get; set; }
	}
}
