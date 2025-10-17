using DAL.EF.Models;
using DAL.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DAL.Repos
{
    
    internal class RecipeRepository : Repository<Recipe, int>, IRecipeRepository
    {
        
        public void UpdateRecipe(Recipe updatedRecipe)
        {
            
            var existingRecipe = GetById(updatedRecipe.Id);
            if (existingRecipe == null) return; 

            
            
            db.Entry(existingRecipe).CurrentValues.SetValues(updatedRecipe);

            
            db.SaveChanges();
        }

        
        public List<Recipe> SearchRecipes(string keyword, int? cuisineId, int? categoryId, int maxPrepTime)
        {
            var query = db.Recipes.AsQueryable();

            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(r => r.Title.Contains(keyword) || r.Ingredients.Contains(keyword));
            }
            if (cuisineId.HasValue)
            {
                query = query.Where(r => r.CuisineId == cuisineId.Value);
            }
            if (categoryId.HasValue)
            {
                query = query.Where(r => r.CategoryId == categoryId.Value);
            }
            if (maxPrepTime > 0)
            {
                query = query.Where(r => r.PrepTimeMinutes <= maxPrepTime);
            }

            return query.ToList();
        }
    }
}