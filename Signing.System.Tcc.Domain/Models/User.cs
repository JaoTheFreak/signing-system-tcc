using System;

namespace Signing.System.Tcc.Domain.Models
{
    public class User
    {
        public User Id { get; set; }        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplayName => $"{FirstName} {LastName}";
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string PicturePath { get; set; }
        #region Identity Shared Fields         
        public string UserName { get; set; }
        public string Email { get; set; }             
        public string NormalizedEmail { get; set; }
        public string NormalizedUserName { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string ConcurrencyStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        #endregion        
    }
}