using FluentValidation;

namespace Volvo.Fleet.FluentValidation
{
    public static class FluentValidationExtension
    {
        public static async Task Run<T>(this AbstractValidator<T> validator, T instance)
        {
            var res = await validator.ValidateAsync(instance);

            if (!res.IsValid)
            {
                var failures = res
                    .Errors
                    .Select(e => e.ErrorMessage)
                    .ToList();

                throw new Exception(string.Join(',', failures));
            }
        }
    }
}