using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMedia.Models
{
	public class Post
	{
		public int Id { get; set; }
		public string Content { get; set; }
		public string Status { get; set; }
		public string? ImagePath { get; set; }

		public int? likes { get; set; }
		public int? loves { get; set; }
		public int? Sads { get; set; }
		public int? Haha { get; set; }
		public int? Angry { get; set; }
		public int? Wow { get; set; }

		public DateTime CreatedAt { get; set; }

		[ForeignKey("User")]
		public string? UserId { get; set; }

		[ForeignKey("Group")]
		public int? GroupId { get; set; }
		public User? User { get; set; }

		public Group? Group { get; set; }

		public ICollection<Comment> Comments { get; set; }
	}
}
