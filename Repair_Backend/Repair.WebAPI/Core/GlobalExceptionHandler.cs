using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Repair.Infrastructure.Exceptions;

namespace FirstCashSolution.CESOP.WebAPI.Core;

    /// <summary>
    /// Encapsulates the exception handling.
    /// </summary>
    public class GlobalExceptionHandler : ExceptionFilterAttribute
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        /// <summary>
        /// Constructs the class.
        /// </summary>
        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Executes the exception handling.
        /// </summary>
        public override Task OnExceptionAsync(ExceptionContext context)
        {
            _logger.LogError(context.Exception, "Exception details:");

            switch (context.Exception)
            {
                case CesopNotAuthorizedException:
                    context.Result = new ForbidResult();
                    break;

                case ArgumentNullException:
                    context.Result = new NotFoundResult();
                    break;

                default: // Any Excption
                    context.Result = new JsonResult(new ExceptionInfoObject(0, "Internal server error", "Please see server log for details", null));
                    context.HttpContext.Response.StatusCode = 500;
                    break;
            }

            return base.OnExceptionAsync(context);
        }
    }
