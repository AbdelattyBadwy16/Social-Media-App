using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using SocialMedia.Application.Response;
using SocialMedia.Core.Models;
using SocialMedia.Infrastructure.Models;
using System.Collections.Generic;

namespace SocialMedia.Application.Repository
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

		public async Task<Response<List<FavouritPost>>> GetAll(string userId)
		{
			var post = await _context.favouritPosts.Where(post => post.UserId == userId).OrderByDescending(post => post.Id).Include("post").ToListAsync();
			return Response<List<FavouritPost>>.Success(post,"",200);
		}

		public async Task<Response<string>> Add(FavouritPost favouritPost)
		{
			try
			{
				await _context.favouritPosts.AddAsync(favouritPost);
				await _context.SaveChangesAsync();
			}catch(Exception ex)
			{
				return Response<string>.Failure("Faild to add fav post.");
			}
			return Response<string>.Success("post added.");
		}

		public async Task<Response<string>> Delete(FavouritPost favouritPost)
		{
			try
			{
				_context.favouritPosts.Remove(favouritPost);
				await _context.SaveChangesAsync();
			}catch(Exception ex)
			{
				return Response<string>.Failure("Faild to delete fav post.");
			}
			return Response<string>.Success("post deleted.");
		}

	}
}
