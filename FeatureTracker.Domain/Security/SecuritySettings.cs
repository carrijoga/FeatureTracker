using System.Security.Cryptography;

namespace FeatureTracker.Domain.Security;

public class SecuritySettings
{
    private int _saltSize = 16;
    private int _hashSize = 32;
    private int _iterations = 250_000;

    public string Pepper { get; set; } = string.Empty;

    public int SaltSize
    {
        get => _saltSize;
        set => _saltSize = value < 16 ? 16 : value;
    }

    public int HashSize
    {
        get => _hashSize;
        set => _hashSize = value < 32 ? 32 : value;
    }

    public int Iterations
    {
        get => _iterations;
        set => _iterations = value < 250_000 ? 250_000 : value;
    }

    public HashAlgorithmType Algorithm { get; set; } = HashAlgorithmType.Sha512;
}

public enum HashAlgorithmType
{
    Sha256,
    Sha384,
    Sha512
}

public static class HashAlgorithmTypeExtensions
{
    public static HashAlgorithmName ToHashAlgorithmName(this HashAlgorithmType type) => type switch
    {
        HashAlgorithmType.Sha256 => HashAlgorithmName.SHA256,
        HashAlgorithmType.Sha384 => HashAlgorithmName.SHA384,
        HashAlgorithmType.Sha512 => HashAlgorithmName.SHA512,
        _ => throw new ArgumentOutOfRangeException(nameof(type))
    };
}
