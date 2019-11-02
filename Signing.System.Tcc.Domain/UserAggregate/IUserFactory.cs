namespace Signing.System.Tcc.Domain.UserAggregate
{
    public interface IUserFactory
    {
        User Create(string email, string passwordHash, string salt, string firstName, string lastName);
    }
}
