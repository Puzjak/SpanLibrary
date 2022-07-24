namespace SpanAcademy.SpanLibrary.Application.Books.Models
{
    public class GetBooksDto
    {
        public bool GetOnlyActive { get; set; } = true;
        public string SortOrder { get; set; }
        public string SearchValue { get; set; }
        public int? Page { get; set; }
        public int? PageSize { get; set; }
        public int? AuthorId { get; set; }
        public int? PublisherId { get; set; }
    }
}
