using BLL.Interfaces;
using DAL;
using DAL.EF.Models;
using DAL.Interfaces;
using System.Linq;

namespace BLL.Services
{
    public class UserFollowService : IUserFollowService
    {
        public string FollowUser(int followerId, int userToFollowId)
        {
            
            if (followerId == userToFollowId) return "You cannot follow yourself.";

            
            var followRepo = DataAccessFactory.GetUserFollowRepo();
            var userRepo = DataAccessFactory.GetUserRepo();

            var userToFollow = userRepo.GetById(userToFollowId);
            if (userToFollow == null) return "User to follow does not exist.";

            if (followRepo.GetAll().Any(f => f.FollowerId == followerId && f.FollowingId == userToFollowId))
            {
                return "You are already following this user.";
            }

            var newFollow = new UserFollow { FollowerId = followerId, FollowingId = userToFollowId };
            followRepo.Create(newFollow);
            return $"You are now following {userToFollow.Name}.";
        }

        public string UnfollowUser(int followerId, int userToUnfollowId)
        {
            
            
            
            IUserFollowRepository followRepo = DataAccessFactory.GetUserFollowRepo();

            
            var existing = followRepo.GetAll().Any(f => f.FollowerId == followerId && f.FollowingId == userToUnfollowId);
            if (!existing)
            {
                return "You are not following this user.";
            }

            
            followRepo.DeleteByUserIds(followerId, userToUnfollowId);
            return "Successfully unfollowed the user.";
        }
    }
}