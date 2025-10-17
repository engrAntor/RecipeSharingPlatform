using BLL.DTOs;
namespace BLL.Interfaces
{
    public interface IUserProfileService
    {
        UserProfileDTO GetProfile(int userId);
        bool UpdateProfile(int userId, UserProfileUpdateDTO profileData);
    }
}