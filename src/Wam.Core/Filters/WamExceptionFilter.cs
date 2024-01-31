using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Wam.Core.DataTransferObjects;
using Wam.Core.Exceptions;

namespace Wam.Core.Filters;

    public class WamExceptionFilter : IActionFilter, IOrderedFilter
    {
        public int Order { get; } = int.MaxValue - 10;

        public void OnActionExecuting(ActionExecutingContext context) { }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is WamException exception)
            {
                context.Result = new ObjectResult(
                    new ExceptionDto(exception.Error.Code, exception.Error.TranslationKey.ToLower(), exception.Message))
                {
                    StatusCode = 409
                };
                context.ExceptionHandled = true;
            }
        }
    }
