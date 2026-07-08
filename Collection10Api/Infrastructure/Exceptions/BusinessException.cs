namespace Collection10Api.Infrastructure.Exceptions;

public class BusinessException : Exception
{
    public BusinessException(string message) : base(message) { }
}
