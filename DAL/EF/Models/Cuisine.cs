using System.ComponentModel.DataAnnotations;
namespace DAL.EF.Models
{
    public class Cuisine
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(100)]
        public string Name { get; set; } 
    }
}