using BLL.Interfaces;
using System.Security.Claims;
using System.Web.Http;

namespace PresentationAPI.Controllers
{
    [Authorize]
    public class FollowController : ApiController
    {
        private readonly IUserFollowService _followService;

        public FollowController(IUserFollowService followService)
        {
            _followService = followService;
        }

        private int GetCurrentUserId()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var userIdClaim = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier);
            return int.Parse(userIdClaim.Value);
        }

        
        [HttpPost]
        [Route("api/actions/follow/{id:int}")]
        public IHttpActionResult Follow(int id)
        {
            var followerId = GetCurrentUserId();
            var resultMessage = _followService.FollowUser(followerId, id);

            if (resultMessage.Contains("now following") || resultMessage.Contains("already following"))
            {
                return Ok(new { Message = resultMessage });
            }
            return BadRequest(resultMessage);
        }

        
        [HttpPost] 
        [Route("api/actions/unfollow/{id:int}")]
        public IHttpActionResult Unfollow(int id)
        {
            var followerId = GetCurrentUserId();
            var resultMessage = _followService.UnfollowUser(followerId, id);

            if (resultMessage.Contains("Successfully unfollowed"))
            {
                return Ok(new { Message = resultMessage });
            }
            return BadRequest(resultMessage);
        }
    }
}