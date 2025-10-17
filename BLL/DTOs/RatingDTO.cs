namespace BLL.DTOs
{
    public class RatingDTO
    {
        public int Id { get; set; }
        public int Score { get; set; }
        public string ReviewText { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; } 
        public int RecipeId { get; set; }
    }
}