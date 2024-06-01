using System.Collections.Immutable;

namespace ELProjectsApp.Shared.Kernel;

public record Error(string Code, string? Message = "")
{
    public static readonly Error None = new Error(string.Empty);
    public static readonly Error NullValue = new Error("Error.NullValue", "The requested value is null.");
}

public record ErrorWithDetails(string Code, string Message, IEnumerable<Error> Details) : Error(Code, Message);
