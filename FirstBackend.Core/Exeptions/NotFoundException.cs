namespace FirstBackend.Core.Exeptions;

public class NotFoundException(string message) : Exception(message)
{
}
