using FluentValidation;
using Volvo.Fleet.Domain.Models.Vehicle;

namespace Volvo.Fleet.VehicleService.Validators
{
    public class VehicleEditionValidator : AbstractValidator<VehicleEditModel>
    {
        public VehicleEditionValidator()
        {
            RuleFor(u => u.Color)
                .NotEmpty()
                .WithMessage("Color is required.")
                .MaximumLength(20)
                .WithMessage("Color must have a maximum of 20 characters.");
        }
    }
}
