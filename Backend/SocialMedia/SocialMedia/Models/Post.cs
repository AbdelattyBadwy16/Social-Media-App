using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMedia.Models
{
	public class Post
	{
		public int Id { get; set; }
		public string Content { get; set; }
		public string Status { get; set; }
		public byte[]? Image { get; set; }

		[ForeignKey("User")]
		public string? UserId { get; set; }

		[ForeignKey("Group")]
		public int? GroupId { get; set; }
		public User? User { get; set; }

		public Group? Group { get; set; }
	}
}
