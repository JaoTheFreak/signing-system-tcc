using System;

namespace Signing.System.Tcc.Domain.UserAggregate
{
    public interface IUser
    {
        int Id { get; }
        string PasswordHash { get; }
        string Salt { get; }
        string Email { get; }
        string FirstName { get; }
        string LastName { get; }
        string DisplayName { get; }
        DateTime CreatedAt { get; }
        DateTime? UpdatedAt { get; }
        //string PicturePath { get; }
    }
}
