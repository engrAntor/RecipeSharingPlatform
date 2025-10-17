using BLL.Interfaces;
using System.Security.Claims;
using System.Web.Http;

namespace PresentationAPI.Controllers
{
    [Authorize]
    [RoutePrefix("api/recommendations")]
    public class RecommendationsController : ApiController
    {
        private readonly IRecommendationService _recommendationService;
        public RecommendationsController(IRecommendationService recommendationService)
        {
            _recommendationService = recommendationService;
        }

        
        [HttpGet, Route("")]
        public IHttpActionResult Get()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var userId = int.Parse(claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value);

            var recommendations = _recommendationService.GetPersonalizedRecommendations(userId);
            return Ok(recommendations);
        }
    }
}