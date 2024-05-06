namespace FirstBackend.Core.Exсeptions;

public class ValidationDataException(string message) : Exception(message)
{
}
