using System.ComponentModel.DataAnnotations;

namespace SocialMedia.Model
{
	public class dtoNewUser
	{
		[Required]
		public string userName {  get; set; }

		[Required]
		public string FirstName { get; set; }

		[Required]
		public string LastName { get; set; }

		[Required]
		public string password { get; set; }
		[Required]
		public string email { get; set; }
		[Required]
		public string Gender { get; set; }


		public string? phoneNumber { get; set; }

		public string Country { get; set; }
	}
}
