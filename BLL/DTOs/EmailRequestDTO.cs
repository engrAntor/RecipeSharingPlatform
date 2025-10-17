using System.ComponentModel.DataAnnotations;
namespace BLL.DTOs
{
    public class EmailRequestDTO
    {
        [Required, EmailAddress]
        public string Email { get; set; }
    }
}