using System;
using Microsoft.AspNetCore.Mvc.Filters;
using HexagonalExample.Domain.Contracts.Adapters;

namespace HexagonalExample.Api.Filters
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        private readonly ILoggerAdapter _loggerAdapter;

        public ExceptionFilter(ILoggerAdapter loggerAdapter)
        {
            _loggerAdapter = loggerAdapter ?? throw new ArgumentNullException(nameof(loggerAdapter));
        }

        public override void OnException(ExceptionContext context)
        {
            _loggerAdapter.Error(context.Exception);

            base.OnException(context);
        }
    }
}
