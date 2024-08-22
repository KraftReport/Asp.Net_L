using CustomCookieAuth.Attributes;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Reflection;

namespace CustomCookieAuth.Services
{
    public class LoggerService
    {

        private readonly ILogger<LoggerService> _logger;

        public LoggerService(ILogger<LoggerService> logger)
        {
            _logger = logger;
        }

        public void LogClassNameAndMessage(ActionExecutingContext context)
        {
            var controllerTypes = context.Controller.GetType();
            var attributes = controllerTypes.GetCustomAttributes<LogHtokeMalAttribute>();

            foreach (var att in attributes)
            {
                _logger.LogInformation($"ClassName:{controllerTypes.Name} , Message:{att.message}");
            }
        }

        public void LogProperties()
        {
            var assembly = Assembly.GetExecutingAssembly();

            var types = assembly.GetTypes();

            foreach(var type in types)
            {
                if (type.IsClass)
                {
                    LogPropertiesOfAClass(type);
                }
            }
        }

        private void LogPropertiesOfAClass(Type type)
        {
            var propertis = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);

            foreach(var prop in propertis)
            {
                var attributeProperties = prop.GetCustomAttributes<LogHtokeMalAttribute>();

                foreach(var att in attributeProperties)
                {
                    _logger.LogInformation(
                        $"ClassName : {type.Name} | " +
                        $"PropName : {prop.Name} | " +
                        $"AttributeMessage : {att.message}");
                }
            }
        }

        public void LogResultOfMethod(ActionExecutedContext context)
        {
            if(context.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor)
            {
                var methodAttributes = controllerActionDescriptor.MethodInfo.GetCustomAttributes<LogHtokeMalAttribute>();

                var methodInfo = controllerActionDescriptor.MethodInfo;
                
                foreach(var  attr in methodAttributes)
                {
                    if(context.Result is Microsoft.AspNetCore.Mvc.ObjectResult result)
                    {
                        _logger.LogInformation(
                            $"MethodReturnValue => {result.Value} |" +
                            $"MethodName => {methodInfo.Name} | " +
                            $"Message => {attr.message}");
                    }
                }
            }

        }
    }
}
