using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.EF.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(100)]
        public string Name { get; set; }
        [Required, StringLength(100)]
        public string Email { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        [Required, StringLength(10)]
        public string Role { get; set; } 
        [Required, StringLength(20)]
        public string Status { get; set; } 
        public string VerificationToken { get; set; }
        public string PasswordResetToken { get; set; }
        public DateTime? ResetTokenExpiry { get; set; }

        
        public User()
        {
            Following = new HashSet<UserFollow>();
            Followers = new HashSet<UserFollow>();
            Recipes = new HashSet<Recipe>();
        }

        [InverseProperty("Follower")]
        public virtual ICollection<UserFollow> Following { get; set; }

        [InverseProperty("Following")]
        public virtual ICollection<UserFollow> Followers { get; set; }

        public virtual ICollection<Recipe> Recipes { get; set; }
    }
}