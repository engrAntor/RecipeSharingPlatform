using DAL.EF.Models;
namespace BLL.Interfaces
{
    public interface INotificationService
    {
        void NotifyNewRecipeFromFollowedUser(Recipe recipe);
        void NotifyNewRatingOnRecipe(Rating rating);
    }
}