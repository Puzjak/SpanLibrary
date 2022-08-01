using Microsoft.EntityFrameworkCore;
using SpanAcademy.SpanLibrary.Application.Publishers.Models;
using SpanAcademy.SpanLibrary.Application.Persistence;
using SpanAcademy.SpanLibrary.Domain;

namespace SpanAcademy.SpanLibrary.Application.Publishers
{
    public class PublisherService : IPublisherService
    {
        private readonly SpanLibraryDbContext _dbContext;

        public PublisherService(SpanLibraryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> PublisherExists(int id, CancellationToken token)
        {
            return await _dbContext.Publishers.Where(Publisher => Publisher.Id == id).AnyAsync(token);
        }

        public async Task<int> CreatePublisher(CreatePublisherDto publisher, CancellationToken token)
        {
            ArgumentNullException.ThrowIfNull(publisher, nameof(publisher));

            Publisher PublisherToCreate = new()
            {
                Name = publisher.Name
            };

            _dbContext.Add(PublisherToCreate);

            await _dbContext.SaveChangesAsync(token);

            return PublisherToCreate.Id;
        }

        public async Task<bool> DeletePublisher(int id, CancellationToken token)
        {
            Publisher PublisherToDelete = await _dbContext.Publishers.FindAsync(new object[] { id }, cancellationToken: token);
            if (PublisherToDelete is null)
                return false;

            _dbContext.Remove(PublisherToDelete);
            await _dbContext.SaveChangesAsync(token);
            return true;
        }

        public async Task<IReadOnlyList<PublisherDto>> GetPublishers(CancellationToken token)
        {
            return await _dbContext.Publishers.AsNoTracking()
                .OrderBy(x => x.Name)
                .Select(x => new PublisherDto
                {
                    Name = x.Name,
                    Id = x.Id
                })
                .ToListAsync(token);
        }

        public async Task<PublisherDto> GetById(int id, CancellationToken token)
        {
            return await _dbContext.Publishers.Where(x => x.Id == id)
                .Select(x => new PublisherDto
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .FirstOrDefaultAsync(token);
        }

        public async Task UpdatePublisher(UpdatePublisherDto publisher, CancellationToken token)
        {
            Publisher PublisherToUpdate = await _dbContext.Publishers.FindAsync(new object[] { publisher.Id }, cancellationToken: token);

            PublisherToUpdate.Name = publisher.Name;

            await _dbContext.SaveChangesAsync(token);
        }
    }
}
