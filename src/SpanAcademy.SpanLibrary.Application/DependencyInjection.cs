using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SpanAcademy.SpanLibrary.Application.Books;
using SpanAcademy.SpanLibrary.Application.Persistence;

namespace SpanAcademy.SpanLibrary.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IBookService, BookService>();

            services.AddDbContext<SpanLibraryDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("SpanLibraryDb"));
            });

            return services;
        }
    }
}
