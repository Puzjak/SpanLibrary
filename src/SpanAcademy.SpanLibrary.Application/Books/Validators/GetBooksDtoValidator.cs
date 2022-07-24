using FluentValidation;
using SpanAcademy.SpanLibrary.Application.Authors;
using SpanAcademy.SpanLibrary.Application.Books.Models;
using SpanAcademy.SpanLibrary.Application.Publishers;

namespace SpanAcademy.SpanLibrary.Application.Books.Validators
{
    public class GetBooksDtoValidator : AbstractValidator<GetBooksDto>
    {
        private static readonly string[] _validSortValues = new[] { "asc", "desc" };
        public GetBooksDtoValidator(IAuthorService authorService, IPublisherService publisherService)
        {
            When(x => x.Page.HasValue, () =>
            {
                RuleFor(x => x.PageSize).NotNull();
                RuleFor(x => x.Page).GreaterThan(0);
            });

            When(x => x.PageSize.HasValue, () =>
            {
                RuleFor(x => x.Page).NotNull();
                RuleFor(x => x.PageSize).GreaterThan(0);
            });

            When(x => x.AuthorId.HasValue, () =>
            {
                RuleFor(x => x.AuthorId).MustAsync((authorId, token) => authorService.AuthorExists(authorId.Value, token))
                    .WithMessage(getBooksDto => $"Author with id '{getBooksDto.AuthorId.Value}' does not exist");
            });


            When(x => x.PublisherId.HasValue, () =>
            {
                RuleFor(x => x.PublisherId).MustAsync((publisherId, token) => publisherService.PublisherExists(publisherId.Value, token))
                    .WithMessage(getBooksDto => $"Publisher with id '{getBooksDto.PublisherId.Value}' does not exist");
            });

            When(x => !string.IsNullOrWhiteSpace(x.SearchValue), () =>
            {
                RuleFor(x => x.SearchValue).MaximumLength(256);
            });

            When(x => !string.IsNullOrWhiteSpace(x.SortOrder), () =>
            {
                RuleFor(x => x.SortOrder).Must(sortOrder => _validSortValues.Contains(sortOrder.ToLowerInvariant()))
                    .WithMessage($"Valid values for '{nameof(GetBooksDto.SortOrder)}' are: {string.Join(", ", _validSortValues.Select(x => $"'{x}'"))}");
            });
        }
    }
}
