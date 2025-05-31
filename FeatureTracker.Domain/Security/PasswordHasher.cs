using System.Security.Cryptography;
using Microsoft.Extensions.Options;

namespace FeatureTracker.Domain.Security;

public sealed class PasswordHasher : IPasswordHasher
{
    #region Properties
    private readonly SecuritySettings _settings;
    private readonly HashAlgorithmName _algorithm;
    #endregion

    #region Constructor
    public PasswordHasher(IOptions<SecuritySettings> options)
    {
        _settings = options?.Value ?? throw new ArgumentNullException(nameof(options));
        _algorithm = _settings.Algorithm.ToHashAlgorithmName();
    }
    #endregion

    #region Methods

    public (string hash, string salt, int iterations) HashPassword(string password)
    {
        var salt = RandomNumberGenerator.GetBytes(_settings.SaltSize);
        var hash = Rfc2898DeriveBytes.Pbkdf2(
            password: password + _settings.Pepper,
            salt: salt,
            iterations: _settings.Iterations,
            hashAlgorithm: _algorithm,
            outputLength: _settings.HashSize);

        return (
            hash: Convert.ToBase64String(hash),
            salt: Convert.ToBase64String(salt),
            iterations:_settings.Iterations);
    }

    public bool Verify(string password, string passwordHash, string passwordSalt, int passwordIterations)
    {
        var salt = Convert.FromBase64String(passwordSalt);
        var expectedHash = Convert.FromBase64String(passwordHash);

        var computedHash = Rfc2898DeriveBytes.Pbkdf2(
            password: password + _settings.Pepper,
            salt: salt,
            iterations: _settings.Iterations,
            hashAlgorithm: _algorithm,
            outputLength: _settings.HashSize);

        return CryptographicOperations.FixedTimeEquals(expectedHash, computedHash);
    }

    #endregion
}
