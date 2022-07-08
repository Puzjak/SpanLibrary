using Microsoft.EntityFrameworkCore;
using SpanAcademy.SpanLibrary.Application.Books.Models;
using SpanAcademy.SpanLibrary.Application.Persistence;
using SpanAcademy.SpanLibrary.Domain;

namespace SpanAcademy.SpanLibrary.Application.Books
{
    public class BookService : IBookService
    {
        private readonly SpanLibraryDbContext _context;

        public BookService(SpanLibraryDbContext context)
        {
            _context = context;
        }
        public async Task<IReadOnlyList<BookDto>> GetBooks(bool onlyActive, CancellationToken token)
        {
            IQueryable<Book> booksQuery = _context.Books.AsNoTracking();

            if (onlyActive)
            {
                booksQuery = booksQuery.Where(book => book.Active!.Value);
            }

            var books = await booksQuery.OrderBy(book => book.Title)
                .Select(book => new BookDto
                {
                    Active = book.Active,
                    Author = book.Author.Name,
                    Description = book.Description,
                    ISBN = book.ISBN,
                    Publisher = book.Publisher.Name,
                    Title = book.Title,
                    YearPublished = book.YearPublished
                })
                .ToListAsync();

            return books;
        }
    }
}
