namespace SpanAcademy.SpanLibrary.Application.Collections
{
    [Serializable]
    public class PagedList<T> : List<T>, IPagedList<T>
    {
        public PagedList(IList<T> source, int page, int pageSize, int? totalCount = null)
        {
            //min allowed page size is 1
            pageSize = Math.Max(pageSize, 1);

            TotalCount = totalCount ?? source.Count;
            TotalPages = TotalCount / pageSize;

            if (TotalCount % pageSize > 0)
                TotalPages++;

            PageSize = pageSize;
            Page = page;
            AddRange(totalCount != null ? source : source.Skip(page * pageSize).Take(pageSize));
        }

        public int Page { get; }

        public int PageSize { get; }

        public int TotalCount { get; }

        public int TotalPages { get; }

        public bool HasPreviousPage => Page > 1;

        public bool HasNextPage => Page < TotalPages;
    }
}
