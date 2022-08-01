
namespace SpanAcademy.SpanLibrary.Application.Collections
{

    /// <summary>
    /// Defines interface which represents paged list
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IPagedList<T> : IReadOnlyList<T>
    {
        int Page { get; }

        int PageSize { get; }

        int TotalCount { get; }

        int TotalPages { get; }

        bool HasPreviousPage { get; }

        bool HasNextPage { get; }
    }
}
