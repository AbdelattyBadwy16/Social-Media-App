using SocialMedia.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMedia.Data
{
	public class User_Post
	{
		public int Id { get; set; }

		public string type { get; set; }

		[ForeignKey("User")]
		public string UserId { get; set; }

		[ForeignKey("Post")]
		public int PostId { get; set; }

		public Post Post { get; set; }
		public User User { get; set; }
	}
}
