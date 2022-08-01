using FluentValidation;
using SpanAcademy.SpanLibrary.Application.Authors.Models;

namespace SpanAcademy.SpanLibrary.Application.Authors.Validators
{
    public class CreateAuthorValidator : AbstractValidator<CreateAuthorDto>
    {
        public CreateAuthorValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(256);
        }
    }
}
