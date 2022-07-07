#nullable disable

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SpanAcademy.SpanLibrary.API.Entities.Base;

namespace SpanAcademy.SpanLibrary.API.Entities
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

    public class BookEntityTypeConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable(nameof(Book));

            builder.Property(book => book.Title).IsRequired().HasMaxLength(256);
            builder.Property(book => book.ISBN).HasMaxLength(256);
            builder.Property(book => book.Description).HasMaxLength(2000);
            builder.Property(book => book.Active).IsRequired().HasDefaultValueSql("1");

            builder.HasOne(book => book.Author)
                .WithMany(author => author.Books)
                .HasForeignKey(book => book.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(book => book.Publisher)
                .WithMany(publisher => publisher.Books)
                .HasForeignKey(book => book.PublisherId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
