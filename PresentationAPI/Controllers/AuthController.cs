using BLL.DTOs;
using BLL.Interfaces;
using System.Web.Http;

namespace PresentationAPI.Controllers
{
    [RoutePrefix("api/auth")]
    public class AuthController : ApiController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost, Route("register"), AllowAnonymous]
        public IHttpActionResult Register(UserRegisterDTO registerData)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var success = _authService.Register(registerData);
            if (success)
            {
                return Ok(new { Message = "Registration successful. Please check your email to verify your account." });
            }
            return BadRequest("User with this email already exists.");
        }

        [HttpGet, Route("verify"), AllowAnonymous]
        public IHttpActionResult Verify([FromUri] string token)
        {
            if (string.IsNullOrEmpty(token)) return BadRequest("Token is missing.");
            var success = _authService.VerifyAccount(token);
            if (success)
            {
                return Ok(new { Message = "Account verified. You can now log in." });
            }
            return BadRequest("Invalid or expired verification token.");
        }

        [HttpPost, Route("login"), AllowAnonymous]
        public IHttpActionResult Login(UserLoginDTO loginData)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var token = _authService.Login(loginData);
            if (token != null)
            {
                return Ok(new { Token = token });
            }
            return Unauthorized();
        }

        [HttpPost, Route("forgot-password"), AllowAnonymous]
        public IHttpActionResult ForgotPassword([FromBody] EmailRequestDTO emailRequest)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            _authService.RequestPasswordReset(emailRequest.Email);
            return Ok(new { Message = "If an account with that email exists, an OTP has been sent." });
        }

        [HttpPost, Route("reset-password"), AllowAnonymous]
        public IHttpActionResult ResetPassword([FromBody] PasswordResetDTO resetData)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var success = _authService.ResetPassword(resetData);
            if (success)
            {
                return Ok(new { Message = "Password has been reset successfully." });
            }
            return BadRequest("Invalid email, OTP, or the OTP has expired.");
        }
    }
}