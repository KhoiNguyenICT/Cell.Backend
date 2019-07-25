using System;
using Cell.Core.Errors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Cell.Common.Errors
{
    public class CellExceptionFilter : ExceptionFilterAttribute
    {
        private readonly ILogger _logger;

        public CellExceptionFilter(ILogger<CellExceptionFilter> logger)
        {
            _logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            CellError cellError;
            switch (context.Exception)
            {
                case CellException exception:
                    context.Exception = null;
                    cellError = exception.Error;
                    _logger.LogError(exception.SerializedErrors);
                    break;
                case UnauthorizedAccessException _:
                    cellError = new CellError("Unauthorized access", StatusCodes.Status401Unauthorized);
                    _logger.LogError(context.Exception, "Unauthorized access.");
                    break;
                default:
                    var env = (IHostingEnvironment)context.HttpContext.RequestServices.GetService(typeof(IHostingEnvironment));
                    var msg = "An unhandled error occurred.";
                    string stack = null;
                    if (!env.IsProduction())
                    {
                        msg = context.Exception.Message;
                        stack = context.Exception.StackTrace;
                    }

                    cellError = new CellError($"{msg} {stack}".Trim());
                    _logger.LogError(new EventId(0), context.Exception, "An unhandled error occurred.");
                    break;
            }

            if (context.HttpContext.Request.Path.Value.StartsWith("/api", StringComparison.OrdinalIgnoreCase))
            {
                context.HttpContext.Response.StatusCode = cellError.StatusCode;
                context.Result = new JsonResult(cellError, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
            }

            base.OnException(context);
        }
    }
}
