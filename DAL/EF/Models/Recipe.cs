using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.EF.Models
{
    public class Recipe
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(200)]
        public string Title { get; set; }
        [Required]
        public string Ingredients { get; set; }
        [Required]
        public string Instructions { get; set; }
        public int PrepTimeMinutes { get; set; }
        public DateTime CreatedAt { get; set; }
        public double AverageRating { get; set; }
        public int TotalRatings { get; set; }

        
        [ForeignKey("Author")]
        public int AuthorId { get; set; }
        public virtual User Author { get; set; }

        
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        [ForeignKey("Cuisine")]
        public int CuisineId { get; set; }
        public virtual Cuisine Cuisine { get; set; }
    }
}