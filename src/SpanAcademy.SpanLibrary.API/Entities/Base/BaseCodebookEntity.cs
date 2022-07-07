#nullable disable

using System.ComponentModel.DataAnnotations;

namespace SpanAcademy.SpanLibrary.API.Entities.Base
{
    public abstract class BaseCodebookEntity : BaseEntity
    {
        [Required]
        [MaxLength(256)]
        public string Name { get; set; }
    }
}
