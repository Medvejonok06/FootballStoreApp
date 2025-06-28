using FluentValidation;
using FootballStoreApp.Dtos;

namespace FootballStoreApp.Validators
{
    public class UpdateItemDtoValidator : AbstractValidator<UpdateItemDto>
    {
        public UpdateItemDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(50);

            RuleFor(x => x.Quantity)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.CurrentOrFinalPrice)
                .GreaterThanOrEqualTo(0);
        }
    }
}
