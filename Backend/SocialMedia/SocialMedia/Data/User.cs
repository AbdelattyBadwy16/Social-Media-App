using Microsoft.AspNetCore.Identity;
using SocialMedia.Data;

namespace SocialMedia.Models
{
	public class User : IdentityUser
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }

		public string? NickName { get; set; }

		public string Gender { get; set; }

		public DateTime? CreatedDate { get; set; }
		public string? BirthDate { get; set; }

		public string Country { get; set; }

		public string? JopTitle { get; set; }
		public string? About {  get; set; }

		public string? IconImagePath { get; set; }
		public string? BackImagePath { get; set; }
		public int Followers {  get; set; }
		public int Following {get; set; }


		public ICollection<Photo> Photos { get; set; }

		public ICollection<Post> Posts { get; set; }

		public ICollection<User_Group> Groups { get; set; }

		public ICollection<Group> MyGroups { get; set; }



	}
}
