﻿namespace FirstBackend.Buiseness.Models.Users.Responses;

public class AuthenticatedResponse
{
    public string Token { get; set; }
    public string RefreshToken { get; set; }
}
