using API.Features.AddToCart;
using Microsoft.AspNetCore.Authentication.Negotiate;

namespace API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container
        builder.Services.AddControllers();

        // Dependency Injection
        builder.Services.AddDependency(builder.Configuration);

        // Swagger
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // MediatR
        builder.Services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(
                typeof(AddToCartCommand).Assembly));

        // FluentValidation
        builder.Services.AddScoped<AddToCartCommandValidator>();

        // HttpContextAccessor
        builder.Services.AddHttpContextAccessor();

        // Session
        builder.Services.AddDistributedMemoryCache();
        builder.Services.AddSession();

        // Authentication
        builder.Services
            .AddAuthentication(NegotiateDefaults.AuthenticationScheme)
            .AddNegotiate();

        // Authorization
        builder.Services.AddAuthorization(options =>
        {
            options.FallbackPolicy = options.DefaultPolicy;
        });

        var app = builder.Build();

        // Swagger
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        // Authentication
        app.UseAuthentication();

        // Session
        app.UseSession();

        // Authorization
        app.UseAuthorization();

        // Controllers
        app.MapControllers();

        app.Run();
    }
}