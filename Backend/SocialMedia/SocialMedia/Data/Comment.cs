using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMedia.Models
{
	public class Comment
	{
		public int Id { get; set; }
		public string Content { get; set; }

		public int? likes { get; set; }
		public int? loves { get; set; }
		public int? Sads { get; set; }
		public int? Haha {  get; set; }
		public int? Angry { get; set; }

		public int? Wow {  get; set; }

		[ForeignKey("post")]
		public int PostId { get; set; }
		public Post	post { get; set; }
	}
}
