using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMedia.Core.Models
{
	public class Friends
	{
		public int Id { get; set; }

		[ForeignKey("User")]
		public string UserId { get; set; }

		[ForeignKey("Follower")]
		public string FollowerId { get; set; }

		public User? User { get; set; }

		public User? Follower {  get; set; }
	}
}
