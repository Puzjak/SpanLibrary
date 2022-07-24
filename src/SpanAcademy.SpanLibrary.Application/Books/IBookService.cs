using SpanAcademy.SpanLibrary.Application.Books.Models;
using SpanAcademy.SpanLibrary.Application.Collections;

namespace SpanAcademy.SpanLibrary.Application.Books
{
    public interface IBookService
    {
        public Task<IPagedList<BookDto>> GetBooks(GetBooksDto getBooksDto, CancellationToken token);
        public Task<BookDto> GetById(int id, CancellationToken token);
        public Task<int> CreateBook(CreateBookDto book, CancellationToken token);
        public Task<bool> DeleteBook(int id, CancellationToken token);
        public Task<bool> BookExists(int id, CancellationToken token);
    }
}
