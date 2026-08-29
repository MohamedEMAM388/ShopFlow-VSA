using API.Data;
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
        
        
        return  services;
    }

}