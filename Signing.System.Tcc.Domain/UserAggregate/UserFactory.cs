namespace Signing.System.Tcc.Domain.UserAggregate
{
    public class UserFactory
    {
        public IUser Create(string email, string passwordHash, string salt, string firstName, string lastName)
        {
            return new User(email, passwordHash, salt, firstName, lastName);
        }
    }
}
