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

        [HttpGet(Name = "GetBooks")]
        public async Task<IReadOnlyList<BookDto>> GetBooks([FromQuery]bool getOnlyActive, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fetching books");

            return await _bookService.GetBooks(getOnlyActive, cancellationToken);
        }
    }
}