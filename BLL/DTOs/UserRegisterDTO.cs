using System.ComponentModel.DataAnnotations;
namespace BLL.DTOs
{
    public class UserRegisterDTO
    {
        [Required, StringLength(100)]
        public string Name { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}