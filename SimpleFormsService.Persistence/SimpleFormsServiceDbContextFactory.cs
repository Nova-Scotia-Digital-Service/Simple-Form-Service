using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using SimpleFormsService.Configuration;

namespace SimpleFormsService.Persistence;

// IDesignTimeDbContextFactory implementation used to support migrations https://docs.microsoft.com/en-gb/ef/core/cli/dbcontext-creation?tabs=dotnet-core-cli
public class SimpleFormsServiceDbContextFactory : IDesignTimeDbContextFactory<SimpleFormsServiceDbContext>
{
    private readonly DbContextOptions<SimpleFormsServiceDbContext> _options;

    public SimpleFormsServiceDbContextFactory() : this(new DbContextOptionsBuilder<SimpleFormsServiceDbContext>()
        .UseNpgsql(AppConfig.Postgres_ConnectionString).Options)
    {}

    public SimpleFormsServiceDbContextFactory(DbContextOptions<SimpleFormsServiceDbContext> options)
    {
        _options = options;
    }

    public SimpleFormsServiceDbContext CreateDbContext(string[] args)
    {
        return new SimpleFormsServiceDbContext(_options);
    }
}

