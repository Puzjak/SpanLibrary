using SpanAcademy.SpanLibrary.Domain.Base;

namespace SpanAcademy.SpanLibrary.Domain
{    
    public class Publisher : BaseCodebookEntity
    {
        public virtual ICollection<Book> Books { get; set; }

        public Publisher()
        {
            Books = new HashSet<Book>();
        }
    }
}
