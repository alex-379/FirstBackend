﻿namespace FirstBackend.Core.Exсeptions;

public class UnauthorizedException(string message = "Доступ запрещён") : Exception(message)
{
}
