using AutoMapper;
using BLL.DTOs;
using BLL.Interfaces;
using DAL;
using DAL.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IMapper _mapper;

        public RecipeService()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            _mapper = config.CreateMapper();
        }
        public List<RecipeDTO> Search(string keyword, int? cuisineId, int? categoryId, int maxPrepTime)
        {
            var recipes = DataAccessFactory.GetRecipeRepo().SearchRecipes(keyword, cuisineId, categoryId, maxPrepTime);
            return _mapper.Map<List<RecipeDTO>>(recipes);
        }

        public List<RecipeDTO> GetAll()
        {
            var recipes = DataAccessFactory.GetRecipeRepo().GetAll();
            return _mapper.Map<List<RecipeDTO>>(recipes);
        }

        public RecipeDTO GetById(int id)
        {
            var recipe = DataAccessFactory.GetRecipeRepo().GetById(id);
            return _mapper.Map<RecipeDTO>(recipe);
        }

        public RecipeDTO Create(CreateRecipeDTO recipeDto, int authorId)
        {
            var recipe = _mapper.Map<Recipe>(recipeDto);
            recipe.AuthorId = authorId;
            recipe.CreatedAt = DateTime.UtcNow;
            recipe.AverageRating = 0;
            recipe.TotalRatings = 0;

            DataAccessFactory.GetRecipeRepo().Create(recipe);

            
            var createdRecipe = DataAccessFactory.GetRecipeRepo().GetById(recipe.Id);
            return _mapper.Map<RecipeDTO>(createdRecipe);
        }

        
        public bool Update(int recipeId, UpdateRecipeDTO recipeDto, int authorId)
        {
            var recipeFromDb = DataAccessFactory.GetRecipeRepo().GetById(recipeId);
            if (recipeFromDb == null || recipeFromDb.AuthorId != authorId)
            {
                return false;
            }

            
            _mapper.Map(recipeDto, recipeFromDb);

            
            DataAccessFactory.GetRecipeRepo().UpdateRecipe(recipeFromDb);

            return true;
        }
        public bool Delete(int recipeId, int authorId)
        {
            
            var recipeFromDb = DataAccessFactory.GetRecipeRepo().GetById(recipeId);
            if (recipeFromDb == null || recipeFromDb.AuthorId != authorId)
            {
                return false;
            }

            
            DataAccessFactory.GetRecipeRepo().Delete(recipeId);
            return true;
        }
    }
}