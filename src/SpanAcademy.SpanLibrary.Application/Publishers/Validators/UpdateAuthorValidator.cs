using FluentValidation;
using SpanAcademy.SpanLibrary.Application.Publishers.Models;

namespace SpanAcademy.SpanLibrary.Application.Publishers.Validators
{
    public class UpdatePublisherValidator : AbstractValidator<UpdatePublisherDto>
    {
        public UpdatePublisherValidator(IPublisherService publisherService)
        {
            RuleFor(x => x.Id).MustAsync(publisherService.PublisherExists).WithMessage(x => $"Publisher with id '{x.Id}' does not exist");
            RuleFor(x => x.Name).NotEmpty().MaximumLength(256);
        }
    }
}
