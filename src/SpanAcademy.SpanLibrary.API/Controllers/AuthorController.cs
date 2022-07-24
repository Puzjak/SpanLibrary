using Microsoft.AspNetCore.Mvc;
using SpanAcademy.SpanLibrary.Application.Authors;
using SpanAcademy.SpanLibrary.Application.Authors.Models;

namespace SpanAcademy.SpanLibrary.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly ILogger<AuthorController> _logger;
        private readonly IAuthorService _authorService;

        public AuthorController(ILogger<AuthorController> logger, IAuthorService authorService)
        {
            _logger = logger;
            _authorService = authorService;
        }

        [HttpGet(Name = nameof(GetAuthors))]
        public async Task<IReadOnlyList<AuthorDto>> GetAuthors(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fetching Authors");

            return await _authorService.GetAuthors(cancellationToken);
        }

        [HttpGet("{id}", Name = nameof(GetAuthor))]
        public async Task<IActionResult> GetAuthor([FromRoute] int id, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fetching Author by id {AuthorId}", id);

            AuthorDto author = await _authorService.GetById(id, cancellationToken);

            return author is not null ? Ok(author) : NotFound();
        }

        [HttpPost(Name = nameof(CreateAuthor))]
        public async Task<IActionResult> CreateAuthor([FromBody] CreateAuthorDto model, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Creating a new Author");

            int authorId = await _authorService.CreateAuthor(model, cancellationToken);

            return CreatedAtAction(nameof(CreateAuthor), new { Id = authorId });
        }

        [HttpPut(Name = nameof(UpdateAuthor))]
        public async Task<IActionResult> UpdateAuthor([FromBody] UpdateAuthorDto model, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Updating Author with id {AuthorId}", model.Id);

            await _authorService.UpdateAuthor(model, cancellationToken);

            return NoContent();
        }

        [HttpDelete("{id}", Name = nameof(DeleteAuthor))]
        public async Task<IActionResult> DeleteAuthor([FromRoute] int id, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Deleting Author with id {AuthorID}", id);

            bool authorDeleted = await _authorService.DeleteAuthor(id, cancellationToken);

            return authorDeleted ? NoContent() : NotFound();
        }
    }
}
