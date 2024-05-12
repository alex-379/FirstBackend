﻿namespace FirstBackend.Business.Interfaces
{
    public interface IPasswordsService
    {
        (string hash, string salt) HashPasword(string password);
        bool VerifyPassword(string password, string hash, string salt);
    }
}