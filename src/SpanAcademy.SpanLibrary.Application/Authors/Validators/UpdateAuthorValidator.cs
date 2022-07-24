using FluentValidation;
using SpanAcademy.SpanLibrary.Application.Authors.Models;

namespace SpanAcademy.SpanLibrary.Application.Authors.Validators
{
    public class UpdateAuthorValidator : AbstractValidator<UpdateAuthorDto>
    {
        public UpdateAuthorValidator(IAuthorService authorService)
        {
            RuleFor(x => x.Id).MustAsync(authorService.AuthorExists).WithMessage(x => $"Author with id '{x.Id}' does not exist");
            RuleFor(x => x.Name).NotEmpty().MaximumLength(256);
        }
    }
}
