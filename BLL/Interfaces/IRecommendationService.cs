using BLL.DTOs;
using System.Collections.Generic;
namespace BLL.Interfaces
{
    public interface IRecommendationService
    {
        List<RecipeDTO> GetPersonalizedRecommendations(int userId);
    }
}