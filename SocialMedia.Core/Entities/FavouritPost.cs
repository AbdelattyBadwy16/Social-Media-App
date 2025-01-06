using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMedia.Core.Models
{
	public class FavouritPost
	{
		public int Id { get; set; }

		[ForeignKey("user")]
		public string UserId { get; set; }

		[ForeignKey("post")]
		public int PostId { get; set; }

		public Post post { get; set; }
		public User user { get; set; }
	}
}
