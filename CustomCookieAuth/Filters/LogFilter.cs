using CustomCookieAuth.Services;
using Microsoft.AspNetCore.Mvc.Filters; 

namespace CustomCookieAuth.Filters
{
    public class LogFilter : IActionFilter
    {
        private readonly LoggerService loggerService;

        public LogFilter(LoggerService _loggerService)
        {
            loggerService = _loggerService;
        }

        void IActionFilter.OnActionExecuted(ActionExecutedContext context)
        {
            loggerService.LogResultOfMethod(context);
        }

        void IActionFilter.OnActionExecuting(ActionExecutingContext context)
        {
            loggerService.LogClassNameAndMessage(context);
            loggerService.LogProperties();
        }
    }
}
