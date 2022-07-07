using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpanAcademy.SpanLibrary.API.Entities;

namespace SpanAcademy.SpanLibrary.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private readonly ILogger<BookController> _logger;
        private readonly SpanLibraryDbContext _dbContext;

        public BookController(ILogger<BookController> logger,
            SpanLibraryDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpGet(Name = "GetBooks")]
        public async Task<IEnumerable<Book>> GetBooks(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fetching books");

            IQueryable<Book> booksQuery = _dbContext.Books.AsNoTracking();

            var books = await booksQuery.Where(book => book.Active!.Value)
                .OrderBy(book => book.Title)
                .ToArrayAsync(cancellationToken);

            return books;
        }
    }
}