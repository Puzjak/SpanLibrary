using SpanAcademy.SpanLibrary.Domain.Base;

namespace SpanAcademy.SpanLibrary.Domain
{    
    public class Author : BaseCodebookEntity
    {
        public virtual ICollection<Book> Books { get; set; }

        public Author()
        {
            Books = new HashSet<Book>();
        }
    }
}
