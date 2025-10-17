using DAL.EF.Models;
using DAL.Interfaces; 
using System.Linq;   

namespace DAL.Repos
{
    
    internal class UserFollowRepository : Repository<UserFollow, int>, IUserFollowRepository
    {
        
        public void DeleteByUserIds(int followerId, int followingId)
        {
            var follow = db.UserFollows.FirstOrDefault(f => f.FollowerId == followerId && f.FollowingId == followingId);
            if (follow != null)
            {
                db.UserFollows.Remove(follow);
                db.SaveChanges();
            }
        }
    }
}