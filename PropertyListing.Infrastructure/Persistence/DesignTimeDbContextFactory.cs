using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace PropertyListing.Infrastructure.Persistence;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        // .env dosyasını yükle
        DotNetEnv.Env.Load();
        
        var connectionString = $"Host={Environment.GetEnvironmentVariable("DB_HOST")};" +
                             $"Port={Environment.GetEnvironmentVariable("DB_PORT")};" +
                             $"Database={Environment.GetEnvironmentVariable("DB_NAME")};" +
                             $"Username={Environment.GetEnvironmentVariable("DB_USER")};" +
                             $"Password={Environment.GetEnvironmentVariable("DB_PASSWORD")}";

        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseNpgsql(connectionString);

        return new ApplicationDbContext(optionsBuilder.Options);
    }
} 