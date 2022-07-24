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

        public async Task<bool> BookExists(int id, CancellationToken token)
        {
            return await _context.Books.Where(book => book.Id == id && book.Active == true).AnyAsync(token);
        }

        public async Task<int> CreateBook(CreateBookDto book, CancellationToken token)
        {
            ArgumentNullException.ThrowIfNull(book, nameof(book));
            Book bookToCreate = new()
            {
                Active = true,
                AuthorId = book.AuthorId,
                Description = book.Description,
                ISBN = book.ISBN,
                PublisherId = book.PublisherId,
                Title = book.Title,
                YearPublished = book.YearPublished,
            };

            _context.Books.Add(bookToCreate);
            await _context.SaveChangesAsync(token);

            return bookToCreate.Id;
        }

        public async Task<bool> DeleteBook(int id, CancellationToken token)
        {
            Book bookToDelete = await _context.Books.Where(book => book.Id == id && book.Active == true).FirstOrDefaultAsync(token);
            if (bookToDelete is null)
                return false;

            bookToDelete.Active = false;
            await _context.SaveChangesAsync(token);

            return true;
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
                    Id = book.Id,
                    Active = book.Active,
                    Author = book.Author.Name,
                    Description = book.Description,
                    ISBN = book.ISBN,
                    Publisher = book.Publisher.Name,
                    Title = book.Title,
                    YearPublished = book.YearPublished
                })
                .ToListAsync(cancellationToken: token);

            return books;
        }

        public async Task<BookDto> GetById(int id, CancellationToken token)
        {
            return await _context.Books.Where(book => book.Id == id)
                .Select(book => new BookDto
                {
                    Active = book.Active,
                    Author = book.Author.Name,
                    Description = book.Description,
                    Id = book.Id,
                    ISBN = book.ISBN,
                    Publisher = book.Publisher.Name,
                    Title = book.Title,
                    YearPublished = book.YearPublished,
                })
                .FirstOrDefaultAsync(token);
        }

        public async Task UpdateBook(UpdateBookDto book, CancellationToken token)
        {
            ArgumentNullException.ThrowIfNull(book, nameof(book));

            Book bookToUpdate = await _context.Books.Where(x => x.Id == book.Id).FirstOrDefaultAsync(token);

            if (bookToUpdate is null)
                throw new NullReferenceException($"Book with id '{book.Id}' does not exist");

            bookToUpdate.AuthorId = book.AuthorId;
            bookToUpdate.Description = book.Description;
            bookToUpdate.ISBN = book.ISBN;
            bookToUpdate.PublisherId = book.PublisherId;
            bookToUpdate.Title = book.Title;
            bookToUpdate.YearPublished = book.YearPublished;

            await _context.SaveChangesAsync(token);
        }
    }
}
