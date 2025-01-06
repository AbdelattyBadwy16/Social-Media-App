using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Optern.Application.Interfaces.ICacheService;
using SocialMedia.Application.Repository;
using SocialMedia.Application.Response;
using SocialMedia.Core.Models;


namespace SocialMedia.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class FriendController : ControllerBase
	{
		private readonly ICacheService _casheService;

		private IFriendRepository friendRepository;
		public FriendController(IFriendRepository _friendRepo ,ICacheService cacheService)
		{
			friendRepository = _friendRepo;
			_casheService = cacheService;
		}
		[HttpPost("addFriend")]

		public async Task<Response<string>> AddFreind(string id, string followerId)
		{
			if (ModelState.IsValid)
			{
				_casheService.RemoveData("following");
				return await friendRepository.Add(id,followerId);
			}
			return Response<string>.Failure("Faild to Add friend");
		}


		[HttpDelete("DeleteFriend")]

		public async Task<Response<string>> DeleteFriend(string userId, string id)
		{
			if (ModelState.IsValid)
			{
				return await friendRepository.Delete(userId, id);
			}
			return Response<string>.Failure("Faild to delete friend");
		}


		[HttpGet("GetUserFollowing")]

		public async Task<Response<List<Friends>>> GetUserFollowing(string id)
		{
			if (ModelState.IsValid)
			{
				return await friendRepository.GetFollowing(id);
			}
			return Response<List<Friends>>.Failure("Faild to get following");
		}


		[HttpGet("GetUserFollower")]

		public async Task<Response<List<Friends>>> GetUserFollower(string id)
		{
			if (ModelState.IsValid)
			{
				return await friendRepository.GetFollower(id);
			}
			return Response<List<Friends>>.Failure("Faild to Get follower");
		}

		[HttpGet("CheckFriend")]

		public async Task<Response<bool>> CheckFriend(string userId, string id)
		{
			if (ModelState.IsValid)
			{
				return await friendRepository.Check(userId, id);
			}
			return Response<bool>.Failure("Faild to Get follower");
		}
	}
}
