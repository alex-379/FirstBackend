﻿namespace FirstBackend.Buiseness.Models.Tokens.Requests;

public class RefreshTokenRequest
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
}
