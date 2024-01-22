using Wam.Core.ErrorCodes;

namespace Wam.Core.Exceptions;

public class WamException(WamErrorCode? error, string message, Exception? innerException) : Exception(message, innerException)
{
    private const string ErrorCodeIsMissingMessage = "WamErrorCode is required";
    public WamErrorCode Error { get; } = error ?? throw new ArgumentNullException(nameof(error), ErrorCodeIsMissingMessage);

    public WamException() : this(null, "An unknown error occurred.", null)
    {
    }

    public WamException(string message) : this(null, message, null)
    {
    }

    public WamException(string message, Exception innerException) : this(null, message, innerException)
    {
    }
}