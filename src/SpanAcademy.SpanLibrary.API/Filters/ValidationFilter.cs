using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog.Context;
using System.Diagnostics;

namespace SpanAcademy.SpanLibrary.API.Filters
{
    public class ValidationFilter : IAsyncActionFilter
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<ValidationFilter> _logger;

        public ValidationFilter(IServiceProvider serviceProvider,
            ILogger<ValidationFilter> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            foreach (var actionArgument in context.ActionArguments)
            {
                Type actionArgumentType = actionArgument.Value?.GetType();
                if (actionArgumentType is null)
                    continue;

                Type paramValidatorType = typeof(IValidator<>).MakeGenericType(actionArgumentType);
                IValidator validator = (IValidator)_serviceProvider.GetService(paramValidatorType);
                if (validator is null)
                    continue;

                _logger.LogInformation("Executing validations for {RequestModel}", actionArgumentType.Name);

                IValidationContext validationContext = (IValidationContext)Activator.CreateInstance(typeof(ValidationContext<>).MakeGenericType(actionArgumentType), actionArgument.Value);
                ValidationResult validationResult = await validator.ValidateAsync(validationContext);
                if (!validationResult.IsValid)
                {
                    var validationErrorsDictionary = validationResult.ToDictionary();
                    _logger.LogWarning("Validations failed with following errors: {ValidationErrors}", validationErrorsDictionary);
                    ValidationProblemDetails problemDetails = new(validationErrorsDictionary)
                    {
                        Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
                    };
                    problemDetails.Extensions.Add("traceId", Activity.Current?.Id ?? context.HttpContext.TraceIdentifier);
                    context.Result = new BadRequestObjectResult(problemDetails);
                    return;
                }
            }
            await next();
        }
    }
}
