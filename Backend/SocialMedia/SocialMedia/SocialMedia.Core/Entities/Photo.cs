using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMedia.Core.Models
{
	public class Photo
	{
		public int Id { get; set; }
		public string? ImagePath { get; set; }

		

		[ForeignKey("User")]
		public string? UserId { get; set; }

		[ForeignKey("Group")]
		public int? GroupId { get; set; }

		public User? User { get; set; }

        public Group? Group { get; set; }


	}
}
