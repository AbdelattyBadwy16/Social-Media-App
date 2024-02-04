using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMedia.Models
{
	public class Group
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string Status { get; set; }
		public byte[]? IconImage { get; set; }
		public byte[]? BackImage { get; set; }

		[ForeignKey("Manager")]
		public string? ManagerId { get; set; }

		public User? Manager { get; set; }

		public ICollection<User_Group>? Members { get; set; }
		public ICollection<Post>? Posts { get; set; }
		public ICollection<Photo>? Photos { get; set; }

	}
}
