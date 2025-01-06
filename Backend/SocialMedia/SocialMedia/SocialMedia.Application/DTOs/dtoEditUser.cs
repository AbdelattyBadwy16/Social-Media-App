namespace SocialMedia.Application.DTOs
{
	public class dtoEditUser
	{
		public string? Id { get; set; }
		public string? FirstName { get; set; }
		public string? LastName { get; set; }
		public string? NickName { get; set; }
		public string? Password { get; set; }
		public string? OldPassword { get; set; }
		public string? BirthDate { get; set; }

		public string? PhoneNumber {get;set;}

		public string? Country { get; set; }

		public string? JopTitle { get; set; }
	}
}
