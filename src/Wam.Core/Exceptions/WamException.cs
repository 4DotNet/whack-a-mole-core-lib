using Wam.Core.ErrorCodes;

namespace Wam.Core.Exceptions;

public abstract class WamException(WamErrorCode error, string message) : Exception(message)
{
    public WamErrorCode Error { get; } = error;
}