namespace Application.Common.Exceptions;

public class ApplicationException : Exception
{
    public ApplicationException(string message) : base(message) { }
    public ApplicationException(string message, Exception innerException) : base(message, innerException) { }
}

public class ValidationException : ApplicationException
{
    public ValidationException(string message) : base(message) { }
    public ValidationException(Dictionary<string, string[]> failures) : base("One or more validation failures have occurred.")
    {
        Failures = failures;
    }

    public Dictionary<string, string[]> Failures { get; }
}

public class NotFoundException : ApplicationException
{
    public NotFoundException(string message) : base(message) { }
    public NotFoundException(string entityType, Guid id) : base($"{entityType} with ID {id} was not found.") { }
}

public class ForbiddenException : ApplicationException
{
    public ForbiddenException(string message) : base(message) { }
}

public class UnauthorizedException : ApplicationException
{
    public UnauthorizedException(string message) : base(message) { }
}

public class ConflictException : ApplicationException
{
    public ConflictException(string message) : base(message) { }
}

public class BusinessRuleException : ApplicationException
{
    public BusinessRuleException(string message) : base(message) { }
}

public class InvalidOperationException : ApplicationException
{
    public InvalidOperationException(string message) : base(message) { }
}
