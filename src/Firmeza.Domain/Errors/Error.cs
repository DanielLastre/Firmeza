namespace Firmeza.Domain.Errors;

public record Error(string Code, string Message)
{
    public static readonly Error None = new(string.Empty, string.Empty);
    public static readonly Error NullValue = new("Error.NullValue", "Null value was provided");
    public static readonly Error Validation = new("Error.Validation", "A validation error occurred");
    public static readonly Error NotFound = new("Error.NotFound", "The requested resource was not found");
    public static readonly Error Conflict = new("Error.Conflict", "A conflict occurred with the current state");
}
