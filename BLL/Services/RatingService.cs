using AutoMapper;
using BLL.DTOs;
using BLL.Interfaces;
using DAL;
using DAL.EF.Models;
using System.Collections.Generic;
using System.Data.Entity; 
using System.Linq;

namespace BLL.Services
{
    public class RatingService : IRatingService
    {
        private readonly IMapper _mapper;
        private readonly INotificationService _notificationService;

        public RatingService()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = config.CreateMapper();

            var emailService = new EmailService();
            _notificationService = new NotificationService(emailService);
        }

        public List<RatingDTO> GetForRecipe(int recipeId)
        {
            
            var ratings = DataAccessFactory.GetRatingRepo()
                .GetAll()
                .Where(r => r.RecipeId == recipeId)
                .ToList(); 
            return _mapper.Map<List<RatingDTO>>(ratings);
        }

        public RatingDTO Add(int recipeId, int userId, CreateRatingDTO ratingDto)
        {
            var ratingRepo = DataAccessFactory.GetRatingRepo();

            
            var existingRating = ratingRepo.GetAll().FirstOrDefault(r => r.RecipeId == recipeId && r.UserId == userId);

            if (existingRating != null)
            {
                existingRating.Score = ratingDto.Score;
                existingRating.ReviewText = ratingDto.ReviewText;
                ratingRepo.Update(existingRating);
            }
            else
            {
                var newRating = _mapper.Map<Rating>(ratingDto);
                newRating.RecipeId = recipeId;
                newRating.UserId = userId;
                ratingRepo.Create(newRating);
            }

            UpdateRecipeAverageRating(recipeId);

            
            
            var finalRating = ratingRepo.GetAll()
                .Include(r => r.User)               
                .Include(r => r.Recipe.Author)      
                .FirstOrDefault(r => r.RecipeId == recipeId && r.UserId == userId);

            
            if (finalRating != null)
            {
                _notificationService.NotifyNewRatingOnRecipe(finalRating);
            }

            
            return _mapper.Map<RatingDTO>(finalRating);
            
        }

        private void UpdateRecipeAverageRating(int recipeId)
        {
            var ratingRepo = DataAccessFactory.GetRatingRepo();
            var recipeRepo = DataAccessFactory.GetRecipeRepo();

            
            var allRatingsForRecipe = ratingRepo.GetAll().Where(r => r.RecipeId == recipeId).ToList();

            var recipe = recipeRepo.GetById(recipeId);
            if (recipe == null) return;

            if (allRatingsForRecipe.Any())
            {
                double newAverage = allRatingsForRecipe.Average(r => r.Score);
                recipe.TotalRatings = allRatingsForRecipe.Count();
                recipe.AverageRating = newAverage;
            }
            else
            {
                recipe.TotalRatings = 0;
                recipe.AverageRating = 0;
            }

            recipeRepo.Update(recipe);
        }
    }
}