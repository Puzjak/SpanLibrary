using Microsoft.AspNetCore.Mvc;
using SpanAcademy.SpanLibrary.Application.Publishers;
using SpanAcademy.SpanLibrary.Application.Publishers.Models;

namespace SpanAcademy.SpanLibrary.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PublisherController : ControllerBase
    {
        private readonly ILogger<PublisherController> _logger;
        private readonly IPublisherService _publisherService;

        public PublisherController(ILogger<PublisherController> logger, IPublisherService publisherService)
        {
            _logger = logger;
            _publisherService = publisherService;
        }

        [HttpGet(Name = nameof(GetPublishers))]
        public async Task<IReadOnlyList<PublisherDto>> GetPublishers(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fetching Publishers");

            return await _publisherService.GetPublishers(cancellationToken);
        }

        [HttpGet("{id}", Name = nameof(GetPublisher))]
        public async Task<IActionResult> GetPublisher([FromRoute] int id, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fetching Publisher by id {PublisherId}", id);

            PublisherDto publisher = await _publisherService.GetById(id, cancellationToken);

            return publisher is not null ? Ok(publisher) : NotFound();
        }

        [HttpPost(Name = nameof(CreatePublisher))]
        public async Task<IActionResult> CreatePublisher([FromBody] CreatePublisherDto model, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Creating a new Publisher");

            int publisherId = await _publisherService.CreatePublisher(model, cancellationToken);

            return CreatedAtAction(nameof(CreatePublisher), new { Id = publisherId });
        }

        [HttpPut(Name = nameof(UpdatePublisher))]
        public async Task<IActionResult> UpdatePublisher([FromBody] UpdatePublisherDto model, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Updating Publisher with id {PublisherId}", model.Id);

            await _publisherService.UpdatePublisher(model, cancellationToken);

            return NoContent();
        }

        [HttpDelete("{id}", Name = nameof(DeletePublisher))]
        public async Task<IActionResult> DeletePublisher([FromRoute] int id, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Deleting Publisher with id {PublisherID}", id);

            bool publisherDeleted = await _publisherService.DeletePublisher(id, cancellationToken);

            return publisherDeleted ? NoContent() : NotFound();
        }
    }
}
