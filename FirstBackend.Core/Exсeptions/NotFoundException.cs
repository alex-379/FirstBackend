﻿namespace FirstBackend.Core.Exсeptions;

public class NotFoundException(string message) : Exception(message)
{
}
