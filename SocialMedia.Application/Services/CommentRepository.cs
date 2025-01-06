using Microsoft.EntityFrameworkCore;
using SocialMedia.Application.Response;
using SocialMedia.Core.Models;
using SocialMedia.Infrastructure.Models;
using SocialMedia.Application.Mapper;
using SocialMedia.Application.DTOs;

namespace SocialMedia.Application.Repository
{
	public class CommentRepository : ICommentRepository
	
	{
		public CommentRepository() { }
		private readonly AppDbContext _context ;
		private readonly IUserRepository _userRepository;
		public CommentRepository(AppDbContext context , IUserRepository userRepository) 
		{ 
			_context = context;
			_userRepository = userRepository;
		}


		public async Task<Response<string>> Add(dtoComment comment)
		{
			User? user = await _userRepository.Get(comment.UserId);
			if (user is null)
			{
				return Response<string>.Failure("Comment Not Found");
			}
			string userName = user.FirstName + " " + user.LastName;

			Comment NewComment = comment.ToComment();
			NewComment.UserImagePath = user.IconImagePath;
			NewComment.UserName = userName;
			try
			{
				await _context.Comments.AddAsync(NewComment);
				await _context.SaveChangesAsync();
			}catch(Exception ex)
			{
				return Response<string>.Failure("Faild to add comment.");
			}
			return Response<string>.Success("Comment added.");
		}

		public async Task<Response<string>> Delete(int id)
		{
			Comment? comment = await Find(id);
			if(comment is null)
			{
				return Response<string>.Failure("Comment Not Found");
			}
			try
			{
				_context.Comments.Remove(comment);
				await _context.SaveChangesAsync();
			}catch(Exception ex)
			{
				return Response<string>.Failure("faild to remove comment.");
			}
			return Response<string>.Success("Comment removed.");
		}

		public Task<Comment?> Find(int id)
		{
			return _context.Comments.FirstOrDefaultAsync(item => item.PostId == id);
		}

		public async Task<Response<List<Comment>>> FindByPost(int Id)
		{
			return Response<List<Comment>>.Success(await _context.Comments.Where(comment => comment.PostId == Id).ToListAsync());
		}
	}
}
