using DAL.EF.Models;
using DAL.Interfaces;
using DAL.Repos;

namespace DAL
{
    public class DataAccessFactory
    {
        public static IRepository<User, int> GetUserRepo()
        {
            return new UserRepository();
        }

        
        public static IRecipeRepository GetRecipeRepo()
        {
            return new RecipeRepository();
        }

        public static IUserFollowRepository GetUserFollowRepo()
        {
            return new UserFollowRepository();
        }
        public static IRepository<Rating, int> GetRatingRepo()
        {
            return new RatingRepository();
        }
        public static IRepository<Notification, int> GetNotificationRepo()
        {
            return new NotificationRepository();
        }
    }
}