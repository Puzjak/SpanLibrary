using Microsoft.EntityFrameworkCore;
using SpanAcademy.SpanLibrary.Application.Authors.Models;
using SpanAcademy.SpanLibrary.Application.Persistence;
using SpanAcademy.SpanLibrary.Domain;

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

        public async Task<int> CreateAuthor(CreateAuthorDto author, CancellationToken token)
        {
            ArgumentNullException.ThrowIfNull(author, nameof(author));

            Author authorToCreate = new()
            {
                Name = author.Name
            };

            _dbContext.Add(authorToCreate);

            await _dbContext.SaveChangesAsync(token);

            return authorToCreate.Id;
        }

        public async Task<bool> DeleteAuthor(int id, CancellationToken token)
        {
            Author authorToDelete = await _dbContext.Authors.FindAsync(new object[] { id }, cancellationToken: token);
            if (authorToDelete is null)
                return false;

            _dbContext.Remove(authorToDelete);
            await _dbContext.SaveChangesAsync(token);
            return true;
        }

        public async Task<IReadOnlyList<AuthorDto>> GetAuthors(CancellationToken token)
        {
            return await _dbContext.Authors.AsNoTracking()
                .OrderBy(x => x.Name)
                .Select(x => new AuthorDto
                {
                    Name = x.Name,
                    Id = x.Id
                })
                .ToListAsync(token);
        }

        public async Task<AuthorDto> GetById(int id, CancellationToken token)
        {
            return await _dbContext.Authors.Where(x => x.Id == id)
                .Select(x => new AuthorDto
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .FirstOrDefaultAsync(token);
        }

        public async Task UpdateAuthor(UpdateAuthorDto author, CancellationToken token)
        {
            Author authorToUpdate = await _dbContext.Authors.FindAsync(new object[] { author.Id }, cancellationToken: token);

            authorToUpdate.Name = author.Name;

            await _dbContext.SaveChangesAsync(token);
        }
    }
}
