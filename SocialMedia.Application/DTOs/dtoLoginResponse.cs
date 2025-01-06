using System.ComponentModel.DataAnnotations;

namespace SocialMedia.Application.DTOs
{
	public class dtoLoginResponse
	{
		public string Id { get; set; }
		public string username { get; set; }
		public string IconImagePath { get; set; }
		
		public string token { get; set; }
	}
}
