using Microsoft.AspNetCore.Mvc;
using SpanAcademy.SpanLibrary.Application.Books;
using SpanAcademy.SpanLibrary.Application.Books.Models;

namespace SpanAcademy.SpanLibrary.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private readonly ILogger<BookController> _logger;
        private readonly IBookService _bookService;

        public BookController(ILogger<BookController> logger, IBookService bookService)
        {
            _logger = logger;
            _bookService = bookService;
        }

        [HttpGet(Name = nameof(GetBooks))]
        public async Task<IReadOnlyList<BookDto>> GetBooks([FromQuery] bool getOnlyActive, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fetching books");

            return await _bookService.GetBooks(getOnlyActive, cancellationToken);
        }

        [HttpGet("{id}", Name = nameof(GetBook))]
        public async Task<IActionResult> GetBook([FromRoute] int id, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fetching book by id {BookId}", id);

            BookDto book = await _bookService.GetById(id, cancellationToken);

            return book is not null ? Ok(book) : NotFound();
        }

        [HttpPost(Name = nameof(CreateBook))]
        public async Task<IActionResult> CreateBook([FromBody] CreateBookDto model, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Creating a new book");

            int bookId = await _bookService.CreateBook(model, cancellationToken);

            return CreatedAtAction(nameof(CreateBook), new { Id = bookId });
        }

        [HttpPut(Name = nameof(UpdateBook))]
        public async Task<IActionResult> UpdateBook([FromBody] UpdateBookDto model, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Updating book with id {BookId}", model.Id);

            await _bookService.UpdateBook(model, cancellationToken);

            return NoContent();
        }

        [HttpDelete("{id}", Name = nameof(DeleteBook))]
        public async Task<IActionResult> DeleteBook([FromRoute] int id, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Deleting book with id {BookID}", id);

            bool bookDeleted = await _bookService.DeleteBook(id, cancellationToken);

            return bookDeleted ? NoContent() : NotFound();
        }
    }
}