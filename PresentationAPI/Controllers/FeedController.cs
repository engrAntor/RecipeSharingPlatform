using BLL.Interfaces;
using System.Security.Claims;
using System.Web.Http;

namespace PresentationAPI.Controllers
{
    [Authorize]
    [RoutePrefix("api/feed")]
    public class FeedController : ApiController
    {
        private readonly IFeedService _feedService;

        public FeedController(IFeedService feedService)
        {
            _feedService = feedService;
        }

        private int GetCurrentUserId()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            return int.Parse(claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value);
        }

        
        [HttpGet, Route("")]
        public IHttpActionResult GetMyFeed()
        {
            var userId = GetCurrentUserId();
            var feed = _feedService.GetUserFeed(userId);
            return Ok(feed);
        }
    }
}