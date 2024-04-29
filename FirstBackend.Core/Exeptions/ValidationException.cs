namespace FirstBackend.Core.Exeptions;

public class ValidationException(string message) : Exception(message)
{
}
