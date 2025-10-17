using BLL.DTOs;
using System.Collections.Generic;
namespace BLL.Interfaces
{
    public interface IFeedService
    {
        List<RecipeDTO> GetUserFeed(int userId);
    }
}