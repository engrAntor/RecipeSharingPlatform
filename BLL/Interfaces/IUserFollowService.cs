namespace BLL.Interfaces
{
    public interface IUserFollowService
    {
        
        string FollowUser(int followerId, int userToFollowId);
        string UnfollowUser(int followerId, int userToUnfollowId);
    }
}