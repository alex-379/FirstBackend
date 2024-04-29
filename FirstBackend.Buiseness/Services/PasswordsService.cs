using FirstBackend.Buiseness.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace FirstBackend.Buiseness.Services;

public class PasswordsService() : IPasswordsService
{
    private const int keySize = 64;
    private const int iterations = 350000;
    private readonly HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;

    public string HashPasword(string secret, string password, out byte[] salt)
    {
        password = $"{password}{secret}";
        salt = RandomNumberGenerator.GetBytes(keySize);
        var hash = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(password),
            salt,
            iterations,
            hashAlgorithm,
            keySize);

        return Convert.ToHexString(hash);
    }

    public bool VerifyPassword(string secret, string password, string hash, byte[] salt)
    {
        password = $"{password}{secret}";
        var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, hashAlgorithm, keySize);

        return CryptographicOperations.FixedTimeEquals(hashToCompare, Convert.FromHexString(hash));
    }
}
