namespace FirstBackend.Buiseness.Interfaces
{
    public interface IPasswordsService
    {
        string HashPasword(string password, out byte[] salt);
    }
}