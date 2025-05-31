namespace FeatureTracker.Domain.Security;

public interface IPasswordHasher
{
    public (string hash, string salt, int iterations) HashPassword(string password);

    public bool Verify(string password, string passwordHash, string passwordSalt, int passwordIterations);
}
