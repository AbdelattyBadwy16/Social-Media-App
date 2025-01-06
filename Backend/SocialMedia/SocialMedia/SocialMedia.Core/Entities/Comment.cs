using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMedia.Core.Models
{
	public class Comment
	{
		public int Id { get; set; }
		public string Content { get; set; }

		public string UserName { get; set; }

		public string UserImagePath { get; set; }

		public string UserId { get; set; }

		[ForeignKey("post")]
		public int PostId { get; set; }
		public Post	post { get; set; }

	}
}
