using SpanAcademy.SpanLibrary.Domain.Base;

namespace SpanAcademy.SpanLibrary.Domain
{
    public class Book : BaseEntity
    {
        public string Title { get; set; }
        public int AuthorId { get; set; }
        public int PublisherId { get; set; }
        public short YearPublished { get; set; }
        public string ISBN { get; set; }
        public string Description { get; set; }
        public bool? Active { get; set; }

        public virtual Author Author { get; set; }
        public virtual Publisher Publisher { get; set; }
    }
}
