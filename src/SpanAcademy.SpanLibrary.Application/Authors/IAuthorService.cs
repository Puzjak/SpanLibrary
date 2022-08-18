namespace SpanAcademy.SpanLibrary.Application.Authors
{
    public interface IAuthorService
    {
        Task<bool> AuthorExists(int id, CancellationToken token);
    }
}
