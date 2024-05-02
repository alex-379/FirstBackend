namespace FirstBackend.Buiseness.Interfaces
{
    public interface IPasswordsService
    {
        string HashPasword(string password, out byte[] salt);
        bool VerifyPassword(string password, string hash, byte[] salt);
    }
}