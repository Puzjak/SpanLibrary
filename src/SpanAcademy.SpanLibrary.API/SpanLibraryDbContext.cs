﻿using Microsoft.EntityFrameworkCore;
using SpanAcademy.SpanLibrary.API.Entities;

#nullable disable

namespace SpanAcademy.SpanLibrary.API
{
    public class SpanLibraryDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Publisher> Publishers { get; set; }

        public SpanLibraryDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Book).Assembly);
        }
    }
}
