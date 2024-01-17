using Wam.Core.ErrorCodes;

namespace Wam.Core.Exceptions;

public class WamException(WamErrorCode error, string message, Exception? innerException) : Exception (message, innerException)
{
    public WamErrorCode Error { get; } = error;

    public WamException() : this(WamErrorCode.Unknown(), "An unknown error occurred.", null)
    {
    }

    public WamException(string message) : this(WamErrorCode.Unknown(), message, null)
    {
    }

    public WamException(string message, Exception innerException) : this(WamErrorCode.Unknown(), message, innerException)
    {
    }
}