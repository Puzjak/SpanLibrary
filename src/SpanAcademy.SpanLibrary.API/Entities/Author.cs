using SpanAcademy.SpanLibrary.API.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpanAcademy.SpanLibrary.API.Entities
{
    [Table(nameof(Author))]
    public class Author : BaseCodebookEntity
    {
        public virtual ICollection<Book> Books { get; set; }

        public Author()
        {
            Books = new HashSet<Book>();
        }
    }
}
