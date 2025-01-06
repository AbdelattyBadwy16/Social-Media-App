namespace SocialMedia.Application.DTOs
{
	public class dtoPost
	{
		public string Content { get; set; }
		public string Status { get; set; }

		public string? UserId { get; set; }

		public int? GroupId { get; set; }
	}
}
