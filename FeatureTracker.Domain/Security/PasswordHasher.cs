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
        var saltBytes = RandomNumberGenerator.GetBytes(_settings.SaltSize);
        var combined = CombinePasswordAndPepper(password, _settings.Pepper);
        var hashBytes = Rfc2898DeriveBytes.Pbkdf2(
            password: combined,
            salt: saltBytes,
            iterations: _settings.Iterations,
            hashAlgorithm: _algorithm,
            outputLength: _settings.HashSize);

        var salt = Convert.ToBase64String(saltBytes);
        var hash = Convert.ToBase64String(hashBytes);

        CryptographicOperations.ZeroMemory(saltBytes);
        CryptographicOperations.ZeroMemory(hashBytes);

        return (hash, salt, iterations: _settings.Iterations);
    }

    public bool Verify(string password, string passwordHash, string passwordSalt, int passwordIterations)
    {
        var salt = Convert.FromBase64String(passwordSalt);
        var combined = CombinePasswordAndPepper(password, _settings.Pepper);
        var expectedHash = Convert.FromBase64String(passwordHash);

        var computedHash = Rfc2898DeriveBytes.Pbkdf2(
            password: combined,
            salt: salt,
            iterations: passwordIterations,
            hashAlgorithm: _algorithm,
            outputLength: _settings.HashSize);

        return CryptographicOperations.FixedTimeEquals(expectedHash, computedHash);
    }

    public static byte[] CombinePasswordAndPepper(string password, string pepper)
    {
        if (string.IsNullOrEmpty(password))
            throw new ArgumentNullException(nameof(password));

        //if (string.IsNullOrEmpty(pepper))
        //    throw new ArgumentNullException(nameof(pepper));

        var passwordBytes = System.Text.Encoding.UTF8.GetBytes(password);
        var pepperBytes = System.Text.Encoding.UTF8.GetBytes(pepper);

        var combined = new byte[passwordBytes.Length + pepperBytes.Length];
        Buffer.BlockCopy(passwordBytes, 0, combined, 0, passwordBytes.Length);
        Buffer.BlockCopy(pepperBytes, 0, combined, passwordBytes.Length, pepperBytes.Length);

        return combined;
    }

    #endregion
}
