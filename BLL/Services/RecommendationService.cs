using BLL.DTOs;
using BLL.Interfaces;
using BLL.RecommendationRules; 
using DAL;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Services
{
    public class RecommendationService : IRecommendationService
    {
        private readonly List<IRecipeRecommendationRule> _rules;

        public RecommendationService()
        {
            
            
            _rules = new List<IRecipeRecommendationRule>
            {
                new CuisineBasedRecommendationRule(),
                new SocialTrendingRecommendationRule()
                
            };
        }

        public List<RecipeDTO> GetPersonalizedRecommendations(int userId)
        {
            var userRepo = DataAccessFactory.GetUserRepo();
            var user = userRepo.GetById(userId);
            if (user == null) return new List<RecipeDTO>();

            
            
            var ratedRecipeIds = DataAccessFactory.GetRatingRepo().GetAll()
                                    .Where(r => r.UserId == userId)
                                    .Select(r => r.RecipeId)
                                    .ToList();
            var createdRecipeIds = DataAccessFactory.GetRecipeRepo().GetAll()
                                    .Where(r => r.AuthorId == userId)
                                    .Select(r => r.Id)
                                    .ToList();
            var interactedRecipeIds = ratedRecipeIds.Union(createdRecipeIds).ToList();

            var allRecommendations = new List<RecipeDTO>();

            
            foreach (var rule in _rules)
            {
                var recommendations = rule.GetRecommendations(user, interactedRecipeIds);
                allRecommendations.AddRange(recommendations);
            }

            
            return allRecommendations.GroupBy(r => r.Id)
                                     .Select(g => g.First())
                                     .ToList();
        }
    }
}