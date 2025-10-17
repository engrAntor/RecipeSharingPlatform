using System;
namespace BLL.DTOs
{
    
    public class RecipeDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Ingredients { get; set; }
        public string Instructions { get; set; }
        public int PrepTimeMinutes { get; set; }
        public DateTime CreatedAt { get; set; }
        public double AverageRating { get; set; }
        public int TotalRatings { get; set; }
        public int AuthorId { get; set; }
        public string AuthorName { get; set; } 
    }
}