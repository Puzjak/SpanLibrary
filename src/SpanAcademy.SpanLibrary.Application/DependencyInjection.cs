using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SpanAcademy.SpanLibrary.Application.Authors;
using SpanAcademy.SpanLibrary.Application.Authors.Models;
using SpanAcademy.SpanLibrary.Application.Authors.Validators;
using SpanAcademy.SpanLibrary.Application.Books;
using SpanAcademy.SpanLibrary.Application.Books.Models;
using SpanAcademy.SpanLibrary.Application.Books.Validators;
using SpanAcademy.SpanLibrary.Application.Persistence;
using SpanAcademy.SpanLibrary.Application.Publishers;
using SpanAcademy.SpanLibrary.Application.Publishers.Models;
using SpanAcademy.SpanLibrary.Application.Publishers.Validators;

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
            services.AddScoped<IValidator<GetBooksDto>, GetBooksDtoValidator>();
            services.AddScoped<IValidator<CreateAuthorDto>, CreateAuthorValidator>();
            services.AddScoped<IValidator<UpdateAuthorDto>, UpdateAuthorValidator>();
            services.AddScoped<IValidator<CreatePublisherDto>, CreatePublisherValidator>();
            services.AddScoped<IValidator<UpdatePublisherDto>, UpdatePublisherValidator>();

            services.AddDbContext<SpanLibraryDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("SpanLibraryDb"));
            });

            return services;
        }
    }
}
