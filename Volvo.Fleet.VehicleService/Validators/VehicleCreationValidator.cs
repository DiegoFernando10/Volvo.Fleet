using FluentValidation;
using Volvo.Fleet.Domain.Models.Vehicle;

namespace Volvo.Fleet.VehicleService.Validators
{
    public class VehicleCreationValidator : AbstractValidator<VehicleModel>
    {
        public VehicleCreationValidator()
        {
            RuleFor(u => u.ChassisSeries)
                .NotEmpty()
                .WithMessage("Chassis Series is required.")
                .MaximumLength(20)
                .WithMessage("Chassis Series must have a maximum of 20 characters.");

            RuleFor(u => u.ChassisNumber)
                .Must(chassisNumber => chassisNumber > 0)
                .WithMessage("Chassis Number is required.")
                .Must(chassisNumber => chassisNumber.ToString().Length <= 17)
                .WithMessage("Chassis Number must have a maximum of 17 characters.");

            RuleFor(u => u.Type)
                .NotEmpty()
                .WithMessage("Type is required.");

            RuleFor(u => u.Color)
                .NotEmpty()
                .WithMessage("Color is required.")
                .MaximumLength(20)
                .WithMessage("Color must have a maximum of 20 characters.");
        }
    }
}
