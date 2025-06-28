using FluentValidation;
using FootballStoreApp.Dtos;

namespace FootballStoreApp.Validators
{
    public class CreateItemDtoValidator : AbstractValidator<CreateItemDto>
    {
        public CreateItemDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(50);

            RuleFor(x => x.Quantity)
                .GreaterThanOrEqualTo(0).WithMessage("Quantity cannot be negative.");

            RuleFor(x => x.CurrentOrFinalPrice)
                .GreaterThanOrEqualTo(0).WithMessage("Price must be non-negative.");
        }
    }
}
