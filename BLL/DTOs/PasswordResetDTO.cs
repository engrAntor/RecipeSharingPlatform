using System.ComponentModel.DataAnnotations;
namespace BLL.DTOs
{
    public class PasswordResetDTO
    {
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Otp { get; set; }
        [Required]
        public string NewPassword { get; set; }
    }
}