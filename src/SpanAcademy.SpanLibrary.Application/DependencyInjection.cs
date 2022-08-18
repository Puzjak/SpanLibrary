using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SpanAcademy.SpanLibrary.Application.Authors;
using SpanAcademy.SpanLibrary.Application.Books;
using SpanAcademy.SpanLibrary.Application.Books.Models;
using SpanAcademy.SpanLibrary.Application.Books.Validators;
using SpanAcademy.SpanLibrary.Application.Persistence;
using SpanAcademy.SpanLibrary.Application.Publishers;

namespace SpanAcademy.SpanLibrary.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<IPublisherService, PublisherService>();

            services.AddScoped<IValidator<CreateBookDto>, CreateBookValidator>();
            services.AddScoped<IValidator<UpdateBookDto>, UpdateBookValidator>();

            services.AddDbContext<SpanLibraryDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("SpanLibraryDb"));
            });

            return services;
        }
    }
}
