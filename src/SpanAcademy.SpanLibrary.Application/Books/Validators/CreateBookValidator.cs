using FluentValidation;
using SpanAcademy.SpanLibrary.Application.Authors;
using SpanAcademy.SpanLibrary.Application.Books.Models;
using SpanAcademy.SpanLibrary.Application.Publishers;

namespace SpanAcademy.SpanLibrary.Application.Books.Validators
{
    public class CreateBookValidator : AbstractValidator<CreateBookDto>
    {
        public CreateBookValidator(IAuthorService authorService, IPublisherService publisherService)
        {
            RuleFor(book => book.Title).NotEmpty().MaximumLength(256);
            RuleFor(book => book.ISBN).MaximumLength(256);
            RuleFor(book => book.Description).MaximumLength(2000);
            RuleFor(book => book.YearPublished).GreaterThanOrEqualTo((short)0)
                .LessThanOrEqualTo((short)DateTime.Now.Year);
            RuleFor(book => book.AuthorId).MustAsync(authorService.AuthorExists)
                .WithMessage(book => $"Author with id '{book.AuthorId}' does not exist");
            RuleFor(book => book.PublisherId).MustAsync(publisherService.PublisherExists)
                .WithMessage(book => $"Publisher with id '{book.PublisherId}' does not exist");
        }
    }
}
