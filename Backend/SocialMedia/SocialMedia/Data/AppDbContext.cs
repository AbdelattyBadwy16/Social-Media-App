using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SocialMedia.Models
{
	public class AppDbContext : IdentityDbContext<User>
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }	

		public DbSet<User> users { get; set; }
		public DbSet<Photo> photos { get; set; }

		public DbSet<User_Group> users_Groups { get; set; }

		public DbSet<Post> posts { get; set; }

		public DbSet<Group> groups { get; set; }

	
	}
}
