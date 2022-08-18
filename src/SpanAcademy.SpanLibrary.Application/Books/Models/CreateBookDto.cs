namespace SpanAcademy.SpanLibrary.Application.Books.Models
{
    public class CreateBookDto
    {
        public string Title { get; set; }
        public int AuthorId { get; set; }
        public int PublisherId { get; set; }
        public short YearPublished { get; set; }
        public string ISBN { get; set; }
        public string Description { get; set; }
    }
}
