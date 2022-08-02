using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SpanAcademy.SpanLibrary.Application.Books.Models;
using SpanAcademy.SpanLibrary.Application.Collections;
using SpanAcademy.SpanLibrary.Application.Persistence;
using SpanAcademy.SpanLibrary.Domain;

namespace SpanAcademy.SpanLibrary.Application.Books
{
    public class BookService : IBookService
    {
        private readonly SpanLibraryDbContext _context;
        private readonly ILogger<BookService> _logger;

        public BookService(SpanLibraryDbContext context, ILogger<BookService> logger)
        {
            _context = context;
            _logger = logger;
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

        public async Task<IPagedList<BookDto>> GetBooks(GetBooksDto getBooksDto, CancellationToken token)
        {
            ArgumentNullException.ThrowIfNull(getBooksDto, nameof(getBooksDto));

            IQueryable<Book> booksQuery = _context.Books.AsNoTracking();

            booksQuery = ApplyFilters(getBooksDto, booksQuery);

            int totalCount = await booksQuery.CountAsync(token);
            booksQuery = ApplySortingAndPaging(getBooksDto, booksQuery);

            var books = await booksQuery
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

            return new PagedList<BookDto>(books, getBooksDto.Page ?? 1, getBooksDto.PageSize ?? int.MaxValue, totalCount);
        }

        private static IQueryable<Book> ApplySortingAndPaging(GetBooksDto getBooksDto, IQueryable<Book> booksQuery)
        {
            booksQuery = getBooksDto.SortOrder switch
            {
                "asc" => booksQuery.OrderBy(x => x.Title),
                "desc" => booksQuery.OrderByDescending(x => x.Title),
                _ => booksQuery.OrderByDescending(x => x.Id)
            };

            if(getBooksDto.Page.HasValue && getBooksDto.PageSize.HasValue)
            {
                booksQuery = booksQuery.Skip((getBooksDto.Page.Value - 1) * getBooksDto.PageSize.Value)
                    .Take(getBooksDto.PageSize.Value);
            }

            return booksQuery;
        }

        private static IQueryable<Book> ApplyFilters(GetBooksDto getBooksDto, IQueryable<Book> booksQuery)
        {
            if (getBooksDto.GetOnlyActive)
            {
                booksQuery = booksQuery.Where(book => book.Active!.Value);
            }

            if (getBooksDto.AuthorId.HasValue)
            {
                booksQuery = booksQuery.Where(book => book.AuthorId == getBooksDto.AuthorId.Value);
            }

            if (getBooksDto.PublisherId.HasValue)
            {
                booksQuery = booksQuery.Where(book => book.PublisherId == getBooksDto.PublisherId.Value);
            }

            if (!string.IsNullOrWhiteSpace(getBooksDto.SearchValue))
            {
                booksQuery = booksQuery.Where(book => book.Title.StartsWith(getBooksDto.SearchValue) || book.ISBN.StartsWith(getBooksDto.SearchValue));
            }

            return booksQuery;
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
    }
}
