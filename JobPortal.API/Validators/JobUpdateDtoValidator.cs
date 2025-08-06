using FluentValidation;
using JobPortal.API.Dtos;

namespace JobPortal.API.Validators
{
    public class JobUpdateDtoValidator : AbstractValidator<JobUpdateDto>
    {
        public JobUpdateDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(100);

            RuleFor(x => x.Company)
                .NotEmpty().WithMessage("Company is required.")
                .MaximumLength(100);

            RuleFor(x => x.Location)
                .NotEmpty().WithMessage("Location is required.")
                .MaximumLength(100);

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(1000);
        }
    }
}
