using Microsoft.AspNetCore.Mvc;
using Serilog.Context;
using SpanAcademy.SpanLibrary.API.Models;
using SpanAcademy.SpanLibrary.Application.Books;
using SpanAcademy.SpanLibrary.Application.Books.Models;
using SpanAcademy.SpanLibrary.Application.Collections;

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
        public async Task<GetBooksResponseModel> GetBooks([FromQuery] GetBooksDto model, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fetching books");

            var books = await _bookService.GetBooks(model, cancellationToken);

            return new GetBooksResponseModel(books);
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

        
        [HttpDelete("{id}", Name = nameof(DeleteBook))]
        public async Task<IActionResult> DeleteBook([FromRoute] int id, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Deleting book with id {BookID}", id);

            bool bookDeleted = await _bookService.DeleteBook(id, cancellationToken);

            return bookDeleted ? NoContent() : NotFound();
        }
    }
}