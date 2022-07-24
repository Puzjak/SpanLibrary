using SpanAcademy.SpanLibrary.Application.Books.Models;

namespace SpanAcademy.SpanLibrary.Application.Books
{
    public interface IBookService
    {
        public Task<IReadOnlyList<BookDto>> GetBooks(bool onlyActive, CancellationToken token);
        public Task<BookDto> GetById(int id, CancellationToken token);
        public Task<int> CreateBook(CreateBookDto book, CancellationToken token);
        public Task UpdateBook(UpdateBookDto book, CancellationToken token);
        public Task<bool> DeleteBook(int id, CancellationToken token);
        public Task<bool> BookExists(int id, CancellationToken token);
    }
}
