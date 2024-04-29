namespace FirstBackend.Buiseness.Interfaces
{
    public interface IPasswordsService
    {
        string HashPasword(string secret, string password, out byte[] salt);
    }
}