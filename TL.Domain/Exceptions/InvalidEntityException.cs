namespace TL.Domain.Exceptions;

public class InvalidEntityException : Exception
{
    public InvalidEntityException(string message) : base(message)
    {
        
    }
}