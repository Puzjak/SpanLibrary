using FluentValidation;
using SpanAcademy.SpanLibrary.Application.Publishers.Models;

namespace SpanAcademy.SpanLibrary.Application.Publishers.Validators
{
    public class CreatePublisherValidator : AbstractValidator<CreatePublisherDto>
    {
        public CreatePublisherValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(256);
        }
    }
}
