using Microsoft.EntityFrameworkCore;
using SpanAcademy.SpanLibrary.Application.Persistence;

namespace SpanAcademy.SpanLibrary.Application.Authors
{
    public class AuthorService : IAuthorService
    {
        private readonly SpanLibraryDbContext _dbContext;

        public AuthorService(SpanLibraryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AuthorExists(int id, CancellationToken token)
        {
            return await _dbContext.Authors.Where(author => author.Id == id).AnyAsync(token);
        }
    }
}
