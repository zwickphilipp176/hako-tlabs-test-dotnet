using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TestApp.Server.Filters
{
    /// <summary>
    /// Runs FluentValidation validators (if registered) against the action arguments
    /// and short-circuits the pipeline with a 400 Bad Request when validation fails.
    /// </summary>
    public class ValidationFilter : IAsyncActionFilter
    {
        private readonly IServiceProvider serviceProvider;

        public ValidationFilter(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            foreach (var argument in context.ActionArguments.Values)
            {
                if (argument == null)
                    continue;

                var validatorType = typeof(IValidator<>).MakeGenericType(argument.GetType());
                if (serviceProvider.GetService(validatorType) is not IValidator validator)
                    continue;

                var validationContext = new ValidationContext<object>(argument);
                var result = await validator.ValidateAsync(validationContext);

                if (!result.IsValid)
                {
                    foreach (var error in result.Errors)
                        context.ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
            }

            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
                return;
            }

            await next();
        }
    }
}
