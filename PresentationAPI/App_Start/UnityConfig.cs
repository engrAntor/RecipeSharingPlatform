using BLL.Interfaces;
using BLL.Services;
using System.Web.Http; 
using Unity;
using Unity.WebApi;

namespace PresentationAPI
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();
            container.RegisterType<IFeedService, FeedService>();
            container.RegisterType<IUserFollowService, UserFollowService>();
            container.RegisterType<IAuthService, AuthService>();
            container.RegisterType<IEmailService, EmailService>();
            container.RegisterType<IUserProfileService, UserProfileService>();
            container.RegisterType<IRecipeService, RecipeService>();
            container.RegisterType<IRatingService, RatingService>();
            container.RegisterType<INotificationService, NotificationService>();
            container.RegisterType<IRecommendationService, RecommendationService>();
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        
        
        }
    }
}