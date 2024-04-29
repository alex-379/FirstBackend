namespace FirstBackend.Core.Exeptions;

public class BadRequestException(string message) : Exception(message)
{
}
