using BLL.DTOs;
using BLL.Interfaces;
using DAL;
using DAL.EF.Models;
using System;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens; 
using BCrypt.Net; 

namespace BLL.Services
{
    public class AuthService : IAuthService
    {
        private readonly IEmailService _emailService;
        private readonly string _jwtSecret = "A_VERY_LONG_AND_SUPER_SECRET_KEY_FOR_RECIPE_API_32_CHARS_OR_MORE";

        public AuthService() 
        {
            _emailService = new EmailService();
        }

        public bool Register(UserRegisterDTO registerData)
        {
            var userRepo = DataAccessFactory.GetUserRepo();
            if (userRepo.GetAll().Any(u => u.Email.Equals(registerData.Email, StringComparison.OrdinalIgnoreCase)))
            {
                return false; 
            }

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(registerData.Password);
            string verificationToken = Guid.NewGuid().ToString();

            var newUser = new User
            {
                Name = registerData.Name,
                Email = registerData.Email,
                PasswordHash = hashedPassword,
                Role = "User",
                Status = "Inactive",
                VerificationToken = verificationToken
            };
            userRepo.Create(newUser);

            string verificationLink = $"https://localhost:44308/api/auth/verify?token={verificationToken}"; 
            string body = $"<h1>Welcome, {newUser.Name}!</h1><p>Please click the link to activate your account: <a href='{verificationLink}'>Activate</a></p>";
            _emailService.SendEmail(newUser.Email, "Activate Your Recipe API Account", body);

            return true;
        }

        public bool VerifyAccount(string token)
        {
            var userRepo = DataAccessFactory.GetUserRepo();
            var user = userRepo.GetAll().FirstOrDefault(u => u.VerificationToken == token);
            if (user == null || user.Status != "Inactive") return false;

            user.Status = "Active";
            user.VerificationToken = null;
            userRepo.Update(user);
            return true;
        }

        public string Login(UserLoginDTO loginData)
        {
            var userRepo = DataAccessFactory.GetUserRepo();
            var user = userRepo.GetAll().FirstOrDefault(u => u.Email.Equals(loginData.Email, StringComparison.OrdinalIgnoreCase));

            if (user == null || !BCrypt.Net.BCrypt.Verify(loginData.Password, user.PasswordHash) || user.Status != "Active")
            {
                return null;
            }
            return GenerateJwtToken(user);
        }

        public bool RequestPasswordReset(string email)
        {
            var userRepo = DataAccessFactory.GetUserRepo();
            var user = userRepo.GetAll().FirstOrDefault(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
            if (user == null) return true; 

            string otp = new Random().Next(100000, 999999).ToString();
            user.PasswordResetToken = BCrypt.Net.BCrypt.HashPassword(otp);
            user.ResetTokenExpiry = DateTime.UtcNow.AddMinutes(10);
            userRepo.Update(user);

            string body = $"<p>Your OTP for password reset is: <strong>{otp}</strong>. It will expire in 10 minutes.</p>";
            _emailService.SendEmail(user.Email, "Your Password Reset OTP", body);
            return true;
        }

        public bool ResetPassword(PasswordResetDTO resetData)
        {
            var userRepo = DataAccessFactory.GetUserRepo();
            var user = userRepo.GetAll().FirstOrDefault(u => u.Email.Equals(resetData.Email, StringComparison.OrdinalIgnoreCase));

            if (user == null || user.PasswordResetToken == null || user.ResetTokenExpiry < DateTime.UtcNow || !BCrypt.Net.BCrypt.Verify(resetData.Otp, user.PasswordResetToken))
            {
                return false;
            }

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(resetData.NewPassword);
            user.PasswordResetToken = null;
            user.ResetTokenExpiry = null;
            userRepo.Update(user);
            return true;
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSecret);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}