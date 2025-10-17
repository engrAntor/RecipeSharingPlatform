using BLL.DTOs;
using BLL.Interfaces;
using System.Security.Claims;
using System.Web.Http;

namespace PresentationAPI.Controllers
{
    [Authorize] 
    [RoutePrefix("api/profile")]
    public class ProfileController : ApiController
    {
        private readonly IUserProfileService _profileService;

        public ProfileController(IUserProfileService profileService)
        {
            _profileService = profileService;
        }

        
        [HttpGet, Route("")]
        public IHttpActionResult Get()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var userIdClaim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            int userId = int.Parse(userIdClaim.Value);

            var profile = _profileService.GetProfile(userId);
            if (profile == null) return NotFound();

            return Ok(profile);
        }

        
        [HttpPut, Route("")]
        public IHttpActionResult Update(UserProfileUpdateDTO profileData)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var claimsIdentity = User.Identity as ClaimsIdentity;
            var userIdClaim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            int userId = int.Parse(userIdClaim.Value);

            var success = _profileService.UpdateProfile(userId, profileData);
            if (success) return Ok(new { Message = "Profile updated successfully." });

            return NotFound();
        }
    }
}