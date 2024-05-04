namespace FirstBackend.Core.Exeptions;

public class ValidationDataException(string message) : Exception(message)
{
}
