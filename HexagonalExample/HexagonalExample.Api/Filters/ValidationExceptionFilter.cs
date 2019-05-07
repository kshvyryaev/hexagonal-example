using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using HexagonalExample.Domain.Entities.Validation;

namespace HexagonalExample.Api.Filters
{
    public class ValidationExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is ValidationException exception)
            {
                context.Result = new JsonResult(exception.Errors)
                {
                    StatusCode = 400
                };

                context.ExceptionHandled = true;
            }

            base.OnException(context);
        }
    }
}
