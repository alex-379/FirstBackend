﻿namespace FirstBackend.Core.Exсeptions;

public class UnauthenticatedException(string message = "Не пройдена аутентификация") : Exception(message)
{
}
