using BLL.Interfaces;
using DAL;
using DAL.EF.Models;
using System;

namespace BLL.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IEmailService _emailService;

        public NotificationService(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public void NotifyNewRecipeFromFollowedUser(Recipe recipe)
        {
            

            var author = DataAccessFactory.GetUserRepo().GetById(recipe.AuthorId);
            if (author == null || author.Followers == null) return;

            foreach (var followerInfo in author.Followers)
            {
                
                var follower = followerInfo.Follower;
                string message = $"{author.Name} just posted a new recipe: '{recipe.Title}'!";

                
                CreateNotification(follower.Id, message);

                
                string subject = "New Recipe from a User You Follow!";
                _emailService.SendEmail(follower.Email, subject, $"<h1>{message}</h1><p>Check it out now!</p>");
            }
        }

        public void NotifyNewRatingOnRecipe(Rating rating)
        {
            
            var recipe = DataAccessFactory.GetRecipeRepo().GetById(rating.RecipeId);
            var author = recipe.Author;
            var rater = rating.User;

            
            if (author.Id == rater.Id) return;

            string message = $"{rater.Name} just left a {rating.Score}-star rating on your recipe: '{recipe.Title}'!";

            
            CreateNotification(author.Id, message);

            
            string subject = "You've received a new rating!";
            _emailService.SendEmail(author.Email, subject, $"<h1>{message}</h1><p>Review: \"{rating.ReviewText}\"</p>");
        }

        
        private void CreateNotification(int userId, string message)
        {
            var notification = new Notification
            {
                UserId = userId,
                Message = message,
                IsRead = false,
                CreatedAt = DateTime.UtcNow
            };
            DataAccessFactory.GetNotificationRepo().Create(notification);
        }
    }
}