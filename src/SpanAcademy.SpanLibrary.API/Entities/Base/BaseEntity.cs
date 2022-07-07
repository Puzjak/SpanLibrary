using System.ComponentModel.DataAnnotations;

namespace SpanAcademy.SpanLibrary.API.Entities.Base
{
    public abstract class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
