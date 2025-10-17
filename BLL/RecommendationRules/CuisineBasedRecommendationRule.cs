using AutoMapper;
using BLL.DTOs;
using BLL.Interfaces;
using DAL;
using DAL.EF.Models;
using System.Collections.Generic;
using System.Linq;

namespace BLL.RecommendationRules
{
    public class CuisineBasedRecommendationRule : IRecipeRecommendationRule
    {
        private readonly IMapper _mapper;
        public CuisineBasedRecommendationRule()
        {  
            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            _mapper = config.CreateMapper();
        }

        public List<RecipeDTO> GetRecommendations(User user, List<int> interactedRecipeIds)
        {
            var ratingRepo = DataAccessFactory.GetRatingRepo();
            var recipeRepo = DataAccessFactory.GetRecipeRepo();

          
            var favoriteCuisine = ratingRepo.GetAll()
                .Where(r => r.UserId == user.Id && r.Score >= 4) 
                .Select(r => r.Recipe.Cuisine) 
                .GroupBy(c => c.Id) 
                .OrderByDescending(g => g.Count()) 
                .Select(g => g.FirstOrDefault()) 
                .FirstOrDefault();

            if (favoriteCuisine == null)
            {
                return new List<RecipeDTO>(); 
            }

           
            var recommendations = recipeRepo.GetAll()
                .Where(r => r.CuisineId == favoriteCuisine.Id && 
                            r.AverageRating >= 4 &&             
                            !interactedRecipeIds.Contains(r.Id)) 
                .Take(5) 
                .ToList();

            return _mapper.Map<List<RecipeDTO>>(recommendations);
        }
    }
}