using BLL.DTOs;
using BLL.Interfaces;
using System.Security.Claims;
using System.Web.Http;

namespace PresentationAPI.Controllers
{
    
    public class RatingsController : ApiController
    {
        private readonly IRatingService _ratingService;

        public RatingsController(IRatingService ratingService)
        {
            _ratingService = ratingService;
        }

        private int GetCurrentUserId()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            if (int.TryParse(claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int userId))
            {
                return userId;
            }
            
            throw new HttpResponseException(System.Net.HttpStatusCode.Unauthorized);
        }

        
        [HttpGet]
        [Route("api/recipes/{recipeId:int}/ratings")] 
        [AllowAnonymous]
        public IHttpActionResult GetRatingsForRecipe(int recipeId)
        {
            var ratings = _ratingService.GetForRecipe(recipeId);
            return Ok(ratings);
        }

        
        [HttpPost]
        [Route("api/recipes/{recipeId:int}/ratings")] 
        [Authorize]
        public IHttpActionResult AddRating(int recipeId, [FromBody] CreateRatingDTO ratingDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userId = GetCurrentUserId();
            var result = _ratingService.Add(recipeId, userId, ratingDto);

            return Ok(result);
        }
    }
}