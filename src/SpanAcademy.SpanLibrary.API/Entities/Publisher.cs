using SpanAcademy.SpanLibrary.API.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpanAcademy.SpanLibrary.API.Entities
{
    [Table(nameof(Publisher))]
    public class Publisher : BaseCodebookEntity
    {
        public virtual ICollection<Book> Books { get; set; }

        public Publisher()
        {
            Books = new HashSet<Book>();
        }
    }
}
