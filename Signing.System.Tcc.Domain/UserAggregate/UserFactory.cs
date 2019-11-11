namespace Signing.System.Tcc.Domain.UserAggregate
{
    public class UserFactory : IUserFactory
    {
        public User Create(string email, string passwordHash, string salt, string firstName, string lastName, string documentNumber)
        {
            return new User(email, passwordHash, salt, firstName, lastName, documentNumber);
        }
    }
}
