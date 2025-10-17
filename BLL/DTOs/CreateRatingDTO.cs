using System.ComponentModel.DataAnnotations;
namespace BLL.DTOs
{
    public class CreateRatingDTO
    {
        [Required]
        [Range(1, 5)]
        public int Score { get; set; }
        [StringLength(500)]
        public string ReviewText { get; set; }
    }
}