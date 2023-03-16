namespace Service;

public class ValidationException : System.Exception
{
    private readonly string[] _message;

    public ValidationException(string message) : base(message)
    {

    }
    public ValidationException(params string[] message)
    {
        _message = message;
    }
    public ValidationException(string message, params object[] args) : this(FormatMessage(message, args))
    {

    }
    public static string FormatMessage(string message, params object[] args) => string.Format(message, args);
}
