using System.ComponentModel.DataAnnotations;
namespace BLL.DTOs
{
    public class UserProfileUpdateDTO
    {
        [Required, StringLength(100)]
        public string Name { get; set; }
    }
}