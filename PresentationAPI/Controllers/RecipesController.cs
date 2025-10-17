using BLL.DTOs;
using BLL.Interfaces;
using System.Net; 
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace PresentationAPI.Controllers
{
    [RoutePrefix("api/recipes")]
    public class RecipesController : ApiController
    {
        private readonly IRecipeService _recipeService;
        public RecipesController()
        {
            
        }

        public RecipesController(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        private int? GetCurrentUserId()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var userIdClaim = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null) return null;
            return int.Parse(userIdClaim.Value);
        }
        [HttpGet]
        [Route("search")]
        [AllowAnonymous] 
        public IHttpActionResult Search([FromUri] string keyword = null, [FromUri] int? cuisineId = null, [FromUri] int? categoryId = null, [FromUri] int maxPrepTime = 0)
        {
            var results = _recipeService.Search(keyword, cuisineId, categoryId, maxPrepTime);
            return Ok(results);
        }
        
        [HttpGet, Route(""), AllowAnonymous]
        public IHttpActionResult GetAll()
        {
            return Ok(_recipeService.GetAll());
        }

        
        [HttpGet, Route("{id:int}"), AllowAnonymous]
        public IHttpActionResult GetById(int id)
        {
            var recipe = _recipeService.GetById(id);
            if (recipe == null) return NotFound();
            return Ok(recipe);
        }

        
        [HttpPost, Route(""), Authorize]
        public IHttpActionResult Create(CreateRecipeDTO recipeDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var authorId = GetCurrentUserId();
            if (authorId == null) return Unauthorized();

            var newRecipe = _recipeService.Create(recipeDto, authorId.Value);
            return Ok(newRecipe);
        }

        
        [HttpPut, Route("{id:int}"), Authorize]
        public IHttpActionResult Update(int id, UpdateRecipeDTO recipeDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var authorId = GetCurrentUserId();
            if (authorId == null) return Unauthorized();

            var success = _recipeService.Update(id, recipeDto, authorId.Value);

            if (success)
            {
                return Ok(new { Message = "Recipe updated successfully." });
            }

            
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.Forbidden, "You do not have permission to modify this resource."));
        }

        
        [HttpDelete, Route("{id:int}"), Authorize]
        public IHttpActionResult Delete(int id)
        {
            var authorId = GetCurrentUserId();
            if (authorId == null) return Unauthorized();

            var success = _recipeService.Delete(id, authorId.Value);

            if (success)
            {
                return Ok(new { Message = "Recipe deleted successfully." });
            }

            
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.Forbidden, "You do not have permission to delete this resource."));
       
        
        }
    }
}