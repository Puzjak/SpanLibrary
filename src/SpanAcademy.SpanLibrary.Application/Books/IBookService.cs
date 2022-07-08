using SpanAcademy.SpanLibrary.Application.Books.Models;

namespace SpanAcademy.SpanLibrary.Application.Books
{
    public interface IBookService
    {
        public Task<IReadOnlyList<BookDto>> GetBooks(bool onlyActive, CancellationToken token);
    }
}
