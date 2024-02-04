using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SocialMedia.Models
{
	public class User_Group
	{
		[Key]
		public int Id { get; set; }

		[ForeignKey("Member")]
		[Column(Order = 1)]
		public string? UserId { get; set; }

		[ForeignKey("Group")]
		
		[Column(Order = 2)]
		public int? GroupId { get; set; }

		public User? Member { get; set; }
		public Group? Group { get; set; }
	}
}
