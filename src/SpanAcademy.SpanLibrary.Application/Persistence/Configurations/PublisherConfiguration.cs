using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SpanAcademy.SpanLibrary.Domain;

namespace SpanAcademy.SpanLibrary.Application.Persistence.Configurations
{
    internal class PublisherConfiguration : IEntityTypeConfiguration<Publisher>
    {
        public void Configure(EntityTypeBuilder<Publisher> builder)
        {
            builder.ToTable(nameof(Publisher));

            builder.SetupCodebookEntity();
        }
    }
}
