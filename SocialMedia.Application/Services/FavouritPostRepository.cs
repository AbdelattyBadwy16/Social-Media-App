using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Optern.Application.Interfaces.ICacheService;
using SocialMedia.Application.Response;
using SocialMedia.Core.Models;
using SocialMedia.Infrastructure.Models;
using System.Collections.Generic;

namespace SocialMedia.Application.Repository
{
	public class FavouritPostRepository : IFavouritPostRepository
	{
		private readonly ICacheService _casheService;
		private readonly AppDbContext _context;
		public FavouritPostRepository(AppDbContext context,ICacheService cacheService) 
		{
			_context = context;
			_casheService = cacheService;
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
			var posts = _casheService.GetData<List<FavouritPost>>("favPosts");
			if (posts is null)
			{
				posts = await _context.favouritPosts.Where(post => post.UserId == userId).OrderByDescending(post => post.Id).Include("post").ToListAsync();
				_casheService.SetData("favPosts", posts);
			}
			return Response<List<FavouritPost>>.Success(posts,"",200);
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
