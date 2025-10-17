using System.ComponentModel.DataAnnotations;
namespace BLL.DTOs
{
    public class CreateRecipeDTO
    {
        [Required, StringLength(200)]
        public string Title { get; set; }
        [Required]
        public string Ingredients { get; set; }
        [Required]
        public string Instructions { get; set; }
        public int PrepTimeMinutes { get; set; }


        
        [Required]
        public int CategoryId { get; set; }

        [Required]
        public int CuisineId { get; set; }
    }
}