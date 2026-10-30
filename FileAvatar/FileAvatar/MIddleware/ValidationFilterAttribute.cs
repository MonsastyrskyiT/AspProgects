using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.ComponentModel.DataAnnotations;

namespace FileAvatar.Middleware
{
    public class ValidationFilterAttribute : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // проходимо по всіх аргументах дії та перевіряємо їх на наявність
            foreach (var argument in context.ActionArguments.Values)
            {
                if (argument is null) continue;

                var argumentType = argument.GetType();

                // шукаємо валідатор для типу аргументу IValidator<T>
                var validatorType = typeof(IValidator<>).MakeGenericType(argumentType);
                var validator = context.HttpContext.RequestServices.GetService(validatorType) as IValidator;

                if (validator != null)
                {
                    // виконуємо асинхронну валідацію аргументу
                    var validationContext = new ValidationContext<object>(argument);
                    var validationResult = await validator.ValidateAsync(validationContext);
                    if (!validationResult.IsValid)
                    {
                        // якщо валідація не пройшла, повертаємо помилку 400 Bad Request з повідомленням про помилки
                        context.Result = new BadRequestObjectResult(validationResult.ToDictionary());
                        return;
                    }
                }
            }
            await next();
        }
    }
}