using BLL.DTOs;
using System.Collections.Generic;
namespace BLL.Interfaces
{
    public interface IRatingService
    {
        List<RatingDTO> GetForRecipe(int recipeId);
        RatingDTO Add(int recipeId, int userId, CreateRatingDTO ratingDto);
    }
}