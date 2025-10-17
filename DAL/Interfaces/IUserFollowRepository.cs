using DAL.EF.Models;
namespace DAL.Interfaces
{
    
    public interface IUserFollowRepository : IRepository<UserFollow, int>
    {
        
        void DeleteByUserIds(int followerId, int followingId);
    }
}