using API.Data;
using API.Features.AddToCart;
using Microsoft.EntityFrameworkCore;

namespace API;

public static class DependencyInjection
{
    public static IServiceCollection AddDependency(this IServiceCollection services , 
                  IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
        });

        services.AddScoped<AddToCartCommandValidator>();
        services.AddHttpContextAccessor();
        services.AddDistributedMemoryCache();
        services.AddSession();
        
        return  services;
    }

}