namespace FirstBackend.Core.Exeptions;

public class ValidationException:Exception
{
    public ValidationException(string message) : base(message)
    {

    }
}
