using DAL.EF.Models;
using System;
using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface IRecipeRepository : IRepository<Recipe, int>
    {
        
        void UpdateRecipe(Recipe updatedRecipe);

        
        List<Recipe> SearchRecipes(string keyword, int? cuisineId, int? categoryId, int maxPrepTime);
    }
}