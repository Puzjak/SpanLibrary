using SpanAcademy.SpanLibrary.Application.Authors.Models;

namespace SpanAcademy.SpanLibrary.Application.Authors
{
    public interface IAuthorService
    {
        public Task<IReadOnlyList<AuthorDto>> GetAuthors(CancellationToken token);
        public Task<AuthorDto> GetById(int id, CancellationToken token);
        public Task<int> CreateAuthor(CreateAuthorDto author, CancellationToken token);
        public Task UpdateAuthor(UpdateAuthorDto author, CancellationToken token);
        public Task<bool> DeleteAuthor(int id, CancellationToken token);
        public Task<bool> AuthorExists(int id, CancellationToken token);
    }
}
