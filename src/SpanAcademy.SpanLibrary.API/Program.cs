using Microsoft.AspNetCore.Mvc;
using Serilog;
using Serilog.Context;
using SpanAcademy.SpanLibrary.API.Filters;
using SpanAcademy.SpanLibrary.Application;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Host.UseSerilog((context, loggerConfig) => loggerConfig.ReadFrom.Configuration(builder.Configuration));

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidationFilter>();
});

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowCors",
                      builder =>
                      {
                          builder.WithOrigins("http://localhost:4200")
                                    .AllowAnyMethod()
                                    .AllowAnyHeader()
                                    .AllowCredentials();
                      });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplication(builder.Configuration);

var app = builder.Build();

app.Use(async (httpContext, next) =>
{
    var traceId = Activity.Current?.Id ?? httpContext.TraceIdentifier;
    using var _traceId = LogContext.PushProperty("TraceId", traceId);
    await next();
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowCors");

app.UseExceptionHandler(exceptionHandlerApp =>
{
    exceptionHandlerApp.Run(async context =>
    {
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;

        var errorResponse = new ProblemDetails
        {
            Title = "Unexpected error has happened",
            Detail = "Unexpected error has happened. Please contact system administrator and provide them with TraceId found in this response",
            Status = StatusCodes.Status500InternalServerError,
            Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1"
        };
        errorResponse.Extensions.Add("traceId", Activity.Current?.Id ?? context.TraceIdentifier);

        await context.Response.WriteAsJsonAsync(errorResponse);
    });
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
