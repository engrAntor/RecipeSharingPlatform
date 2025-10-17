using BLL.DTOs;
using DAL.EF.Models;
using System.Collections.Generic;

namespace BLL.Interfaces
{
    
    public interface IRecipeRecommendationRule
    {
        List<RecipeDTO> GetRecommendations(User user, List<int> interactedRecipeIds);
    }
}