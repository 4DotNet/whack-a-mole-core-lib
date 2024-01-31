using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Wam.Core.DataTransferObjects;

namespace Wam.Core.Exceptions;

public class WamExceptionHandler(ILogger<WamExceptionHandler> logger) : IExceptionHandler
{
    public ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is WamException wamException)
        {
            logger.LogError(exception, "WamException caught");
            var dto = new ExceptionDto(
                               wamException.Error.Code,
                                              wamException.Error.TranslationKey,
                                              wamException.Message
                                              );

            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.WriteAsJsonAsync(dto, cancellationToken);
            return ValueTask.FromResult(true);
        }


        return ValueTask.FromResult(false);
    }
}