namespace FirstBackend.Core.Exсeptions;

public class BadRequestException(string message) : Exception(message)
{
}
