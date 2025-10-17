using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.EF.Models
{
    public class UserFollow
    {
        [Key, Column(Order = 0)]
        [ForeignKey("Follower")]
        public int FollowerId { get; set; } 

        [Key, Column(Order = 1)]
        [ForeignKey("Following")]
        public int FollowingId { get; set; } 

        public virtual User Follower { get; set; }
        public virtual User Following { get; set; }
    }
}