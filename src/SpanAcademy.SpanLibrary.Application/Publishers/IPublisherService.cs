namespace SpanAcademy.SpanLibrary.Application.Publishers
{
    public interface IPublisherService
    {
        Task<bool> PublisherExists(int id, CancellationToken token);
    }
}
