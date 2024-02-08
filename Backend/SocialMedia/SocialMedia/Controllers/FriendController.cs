using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Data;
using SocialMedia.Models;

namespace SocialMedia.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class FriendController : ControllerBase
	{
		public FriendController(AppDbContext DB)
		{
			_DB = DB;
		}
		private readonly AppDbContext _DB;

		[HttpPost("addFriend")]

		public async Task<IActionResult> AddFreind(string id, string followerId)
		{
			Friends friend = new Friends()
			{
				UserId = id,
				FollowerId = followerId
			};
			await _DB.friends.AddAsync(friend);
			var user = _DB.users.FirstOrDefault(user => user.Id == id);
			if (user != null)
			{
				user.Following++;
			}
			user = _DB.users.FirstOrDefault(user => user.Id == followerId);
			if (user != null)
			{
				user.Followers++;
			}
			await _DB.SaveChangesAsync();
			return Ok(ModelState);
		}


		[HttpDelete("DeleteFriend")]

		public async Task<IActionResult> DeleteFriend(string userId, string id)
		{
			Friends friend  = _DB.friends.FirstOrDefault((item) => item.UserId == userId && item.FollowerId == id);
			_DB.friends.Remove(friend);

			var user = _DB.users.FirstOrDefault(user => user.Id == userId);
			if (user != null)
			{
				user.Following--;
			}
			user = _DB.users.FirstOrDefault(user => user.Id == id);
			if (user != null)
			{
				user.Followers--;
			}
			_DB.SaveChanges();
			return Ok(ModelState);

		}


		[HttpGet("GetUserFollowing")]

		public async Task<IActionResult> GetUserFollowing(string id)
		{
			var users = _DB.friends.Where((item) => item.UserId == id);		

			return Ok(users);

		}


		[HttpGet("GetUserFollower")]

		public async Task<IActionResult> GetUserFollower(string id)
		{
			var users = _DB.friends.Where((item) => item.FollowerId== id);

			return Ok(users);

		}

		[HttpGet("CheckFriend")]

		public async Task<IActionResult> CheckFriend(string userId, string id)
		{
			var users = _DB.friends.Where((item) => item.UserId == userId);
			var res = false;
			foreach(var user in users)
			{
				if (user.FollowerId == id) res = true;
			}

			return Ok(res);

		}
	}
}
