using AutoMapper;
using BLL.DTOs;
using BLL.Interfaces;
using DAL;
using DAL.EF.Models;
using System.Collections.Generic;
using System.Linq;

namespace BLL.RecommendationRules
{
    public class SocialTrendingRecommendationRule : IRecipeRecommendationRule
    {
        private readonly IMapper _mapper;
        public SocialTrendingRecommendationRule()
        {  
            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            _mapper = config.CreateMapper();
        }

        public List<RecipeDTO> GetRecommendations(User user, List<int> interactedRecipeIds)
        {
            
            var followingIds = user.Following.Select(f => f.FollowingId).ToList();
            if (!followingIds.Any())
            {
                return new List<RecipeDTO>(); 
            }

            var recipeRepo = DataAccessFactory.GetRecipeRepo();

            
            var recommendations = recipeRepo.GetAll()
                .Where(r => followingIds.Contains(r.AuthorId) && 
                            r.AverageRating >= 4.5 &&            
                            !interactedRecipeIds.Contains(r.Id)) 
                .OrderByDescending(r => r.CreatedAt) 
                .Take(5)
                .ToList();

            return _mapper.Map<List<RecipeDTO>>(recommendations);
        }
    }
}