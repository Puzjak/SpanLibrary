using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SpanAcademy.SpanLibrary.Domain.Base;

namespace SpanAcademy.SpanLibrary.Application.Persistence.Configurations
{
    internal static class EntityBuilderExtensions
    {
        public static void SetupCodebookEntity<T>(this EntityTypeBuilder<T> builder) where T: BaseCodebookEntity
        {
            builder.Property(x => x.Name).HasMaxLength(256).IsRequired();
            builder.SetupEntity();
        }

        public static void SetupEntity<T>(this EntityTypeBuilder<T> builder) where T: BaseEntity
        {
            builder.HasKey(x => x.Id);
        }
    }
}
