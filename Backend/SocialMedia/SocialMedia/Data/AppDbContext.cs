using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Data;

namespace SocialMedia.Models
{
	public class AppDbContext : IdentityDbContext<User>
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
		public AppDbContext() : base() { }
		public DbSet<User> users { get; set; }
		public DbSet<Photo> photos { get; set; }

		public DbSet<User_Group> users_Groups { get; set; }

		public DbSet<Post> posts { get; set; }

		public DbSet<Group> groups { get; set; }
		public DbSet<User_Post> user_Posts { get; set; }

		public DbSet<Comment> Comments { get; set; }
		public DbSet<Friends> friends { get; set; }
		public DbSet<FavouritPost> favouritPosts { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer("Server = . ; Database = SocailMediaDB;TrustServerCertificate=True;Encrypt=false;User Id=sa;password=abdo@011429");
			base.OnConfiguring(optionsBuilder);
		}

	}
}
