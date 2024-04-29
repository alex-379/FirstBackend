namespace FirstBackend.Core.Exeptions;

public class UnauthorizedException(string message) : Exception(message)
{
}
