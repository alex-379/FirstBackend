namespace FirstBackend.Core.Exсeptions;

public class ConflictException(string message) : Exception(message)
{
}
