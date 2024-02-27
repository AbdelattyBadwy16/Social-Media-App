using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Data;
using SocialMedia.Models;
using SocialMedia.Repository;

namespace SocialMedia.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class FriendController : ControllerBase
	{
		public FriendController(AppDbContext DB , IFriendRepository _friendRepo)
		{
			friendRepository = _friendRepo;
		}
		private IFriendRepository friendRepository;
		[HttpPost("addFriend")]

		public IActionResult AddFreind(string id, string followerId)
		{
			Friends friend = new Friends()
			{
				UserId = id,
				FollowerId = followerId
			};
			friendRepository.Add(friend);
			friendRepository.UpdateFollower(id, followerId,1);
			return Ok(ModelState);
		}


		[HttpDelete("DeleteFriend")]

		public async Task<IActionResult> DeleteFriend(string userId, string id)
		{
			Friends? friend = await friendRepository.Find(userId,id);
			friendRepository.Delete(friend, userId, id);
			friendRepository.UpdateFollower(userId, id, -1);
			return Ok(ModelState);

		}


		[HttpGet("GetUserFollowing")]

		public IActionResult GetUserFollowing(string id)
		{
			var users = friendRepository.GetFollowing(id);
			return Ok(users);

		}


		[HttpGet("GetUserFollower")]

		public IActionResult GetUserFollower(string id)
		{
			var users = friendRepository.GetFollower(id);
			return Ok(users);

		}

		[HttpGet("CheckFriend")]

		public IActionResult CheckFriend(string userId, string id)
		{
			bool IsFreind= friendRepository.Check(userId, id);
			return Ok(IsFreind);

		}
	}
}
