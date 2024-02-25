using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using SocialMedia.Data;
using SocialMedia.Models;
using System.Collections.Generic;

namespace SocialMedia.Repository
{
	public class FavouritPostRepository : IFavouritPostRepository
	{
		public FavouritPostRepository() { }
		AppDbContext _context = new AppDbContext();

		public FavouritPost? Find(string userId ,int PostId)
		{
			FavouritPost? post = _context.favouritPosts.FirstOrDefault(post => post.UserId == userId && post.PostId == PostId);
			return post;
		}

		public List<FavouritPost> GetAll(string userId)
		{
			List< FavouritPost> post = _context.favouritPosts.Where(post => post.UserId == userId).OrderByDescending(post => post.Id).Include("post").ToList();
			return post;
		}

		public async void Add(FavouritPost favouritPost)
		{
			await _context.favouritPosts.AddAsync(favouritPost);
			await _context.SaveChangesAsync();
		}

		public async void Delete(FavouritPost favouritPost)
		{
			_context.favouritPosts.Remove(favouritPost);
			await _context.SaveChangesAsync();
		}

	}
}
