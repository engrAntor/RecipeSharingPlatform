using BLL.DTOs;
using System.Collections.Generic;
namespace BLL.Interfaces
{
    public interface IRecipeService
    {
        List<RecipeDTO> GetAll();
        RecipeDTO GetById(int id);
        RecipeDTO Create(CreateRecipeDTO recipe, int authorId);
        bool Update(int recipeId, UpdateRecipeDTO recipe, int authorId);
        bool Delete(int recipeId, int authorId);
        List<RecipeDTO> Search(string keyword, int? cuisineId, int? categoryId, int maxPrepTime);
    }
}