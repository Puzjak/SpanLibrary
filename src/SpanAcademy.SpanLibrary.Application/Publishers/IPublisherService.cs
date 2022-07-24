using SpanAcademy.SpanLibrary.Application.Publishers.Models;

namespace SpanAcademy.SpanLibrary.Application.Publishers
{
    public interface IPublisherService
    {
        public Task<IReadOnlyList<PublisherDto>> GetPublishers(CancellationToken token);
        public Task<PublisherDto> GetById(int id, CancellationToken token);
        public Task<int> CreatePublisher(CreatePublisherDto publisher, CancellationToken token);
        public Task UpdatePublisher(UpdatePublisherDto publisher, CancellationToken token);
        public Task<bool> DeletePublisher(int id, CancellationToken token);
        public Task<bool> PublisherExists(int id, CancellationToken token);
    }
}
