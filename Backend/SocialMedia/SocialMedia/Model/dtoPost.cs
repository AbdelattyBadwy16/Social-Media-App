namespace SocialMedia.Model
{
	public class dtoPost
	{
		public string Content { get; set; }
		public string Status { get; set; }

		public IFormFile file { get; set; }
		public string? UserId { get; set; }

		public int? GroupId { get; set; }
	}
}
