using FirstBackend.Buiseness.Configuration;
using FirstBackend.Buiseness.Interfaces;
using FirstBackend.Core.Constants;
using System.Collections;
using System.Security.Cryptography;
using System.Text;

namespace FirstBackend.Buiseness.Services;

public class PasswordsService(SecretSettings secret) : IPasswordsService
{
    private readonly SecretSettings _secret = secret;
    private readonly HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;

    public (string hash, string salt) HashPasword(string password)
    {
        password = $"{password}{_secret.SecretPassword}";
        var salt = RandomNumberGenerator.GetBytes(PasswordSettings.KeySize);
        var hash = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(password),
            salt,
            PasswordSettings.Iterations,
            hashAlgorithm,
            PasswordSettings.KeySize);

        return (Convert.ToHexString(hash), Convert.ToHexString(salt));
    }

    public bool VerifyPassword(string password, string hash, string salt)
    {
        password = $"{password}{_secret.SecretPassword}";
        var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, Convert.FromHexString(salt), PasswordSettings.Iterations, hashAlgorithm, PasswordSettings.KeySize);

        return CryptographicOperations.FixedTimeEquals(hashToCompare, Convert.FromHexString(hash));
    }
}
