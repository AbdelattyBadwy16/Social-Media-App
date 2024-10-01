using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using SocialMedia.Data;
using SocialMedia.Models;
using System.Collections.Generic;

namespace SocialMedia.Repository
{
	public class FavouritPostRepository : IFavouritPostRepository
	{
		private readonly AppDbContext _context;
		public FavouritPostRepository(AppDbContext context) 
		{
			_context = context;
		}

		public async Task<FavouritPost?> Find(string userId ,int PostId)
		{
			FavouritPost? post = new FavouritPost();
			try
			{
				 post = await _context.favouritPosts.FirstOrDefaultAsync(post => post.UserId == userId && post.PostId == PostId);
			}catch(Exception ex)
			{
				throw new Exception(ex.Message);
			}
			return post;
		}

		public async Task<List<FavouritPost>> GetAll(string userId)
		{
			List< FavouritPost> post = new List<FavouritPost>();
			try
			{
				post = await _context.favouritPosts.Where(post => post.UserId == userId).OrderByDescending(post => post.Id).Include("post").ToListAsync();
			}catch(Exception ex)
			{
				throw new Exception(ex.Message);
			}
			return post;
		}

		public async Task Add(FavouritPost favouritPost)
		{
			try
			{
				await _context.favouritPosts.AddAsync(favouritPost);
				await _context.SaveChangesAsync();
			}catch(Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task Delete(FavouritPost favouritPost)
		{
			try
			{
				_context.favouritPosts.Remove(favouritPost);
				await _context.SaveChangesAsync();
			}catch(Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

	}
}
