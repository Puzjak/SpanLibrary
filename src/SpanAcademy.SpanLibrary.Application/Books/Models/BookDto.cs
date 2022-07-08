namespace SpanAcademy.SpanLibrary.Application.Books.Models
{
    public class BookDto
    {
        public string Title { get; set; }
        public short YearPublished { get; set; }
        public string ISBN { get; set; }
        public string Description { get; set; }
        public bool? Active { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
    }
}
