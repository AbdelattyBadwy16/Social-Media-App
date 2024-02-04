using Microsoft.AspNetCore.Identity;

namespace SocialMedia.Models
{
	public class User : IdentityUser
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }

		public string? NickName { get; set; }

		public string Gender { get; set; }

		public DateTime? CreatedDate { get; set; }
		public DateTime? BirthDate { get; set; }

		public string Country { get; set; }

		public string? JopTitle { get; set; }
		public string? About {  get; set; }
		public byte[]? BackImage { get; set; }
		public byte[]? IconImage { get; set; }

		public ICollection<User> Friends { get; set; }

		public ICollection<Photo> Photos { get; set; }

		public ICollection<Post> Posts { get; set; }

		public ICollection<User_Group> Groups { get; set; }

		public ICollection<Group> MyGroups { get; set; }


	}
}
