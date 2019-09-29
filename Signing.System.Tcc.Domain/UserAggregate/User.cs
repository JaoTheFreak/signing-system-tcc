using System;

namespace Signing.System.Tcc.Domain.UserAggregate
{
    public class User : IUser
    {
        public User(string email, string passwordHash, string salt, string firstName, string lastName)
        {
            Email = email;

            PasswordHash = passwordHash;

            Salt = salt;

            FirstName = firstName;

            LastName = lastName;
            
            CreatedAt = DateTime.Now;
        }

        public int Id { get; private set; }        
        public string PasswordHash { get; private set; }
        public string Salt { get; private set; }
        public string Email { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string DisplayName => $"{FirstName} {LastName}";
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }     
    }
}