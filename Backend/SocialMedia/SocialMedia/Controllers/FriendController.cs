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
		private IFriendRepository friendRepository;
		public FriendController(IFriendRepository _friendRepo)
		{
			friendRepository = _friendRepo;
		}
		[HttpPost("addFriend")]

		public async Task<IActionResult> AddFreind(string id, string followerId)
		{
			if (ModelState.IsValid)
			{
				Friends friend = new Friends()
				{
					UserId = id,
					FollowerId = followerId
				};
				await friendRepository.Add(friend);
				await friendRepository.UpdateFollower(id, followerId, 1);
				return Ok(ModelState);
			}
			return BadRequest(ModelState);
		}


		[HttpDelete("DeleteFriend")]

		public async Task<IActionResult> DeleteFriend(string userId, string id)
		{
			if (ModelState.IsValid)
			{
				Friends? friend = await friendRepository.Find(userId, id);
				if (friend == null) return NotFound();
				await friendRepository.Delete(friend, userId, id);
				await friendRepository.UpdateFollower(userId, id, -1);
				return Ok();
			}
			return BadRequest(ModelState);
		}


		[HttpGet("GetUserFollowing")]

		public async Task<IActionResult> GetUserFollowing(string id)
		{
			if (ModelState.IsValid)
			{
				var users = await friendRepository.GetFollowing(id);
				return Ok(users);
			}
			return BadRequest(ModelState);
		}


		[HttpGet("GetUserFollower")]

		public async Task<IActionResult> GetUserFollower(string id)
		{
			if (ModelState.IsValid)
			{
				var users = await friendRepository.GetFollower(id);
				return Ok(users);
			}
			return BadRequest(ModelState);

		}

		[HttpGet("CheckFriend")]

		public async Task<IActionResult> CheckFriend(string userId, string id)
		{
			if (ModelState.IsValid)
			{
				bool IsFreind = await friendRepository.Check(userId, id);
				return Ok(IsFreind);
			}
			return BadRequest(ModelState);
		}
	}
}
