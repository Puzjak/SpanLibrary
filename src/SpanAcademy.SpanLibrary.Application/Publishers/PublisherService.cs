using Microsoft.EntityFrameworkCore;
using SpanAcademy.SpanLibrary.Application.Persistence;

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
            return await _dbContext.Authors.Where(author => author.Id == id).AnyAsync(token);
        }
    }
}
