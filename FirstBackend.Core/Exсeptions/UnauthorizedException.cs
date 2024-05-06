namespace FirstBackend.Core.Exсeptions;

public class UnauthorizedException(string message) : Exception(message)
{
}
