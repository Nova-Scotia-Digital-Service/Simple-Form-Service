using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace SimpleFormsService.Configuration;

public static class AppConfig
{
    public static IConfigurationRoot AppSettings => LazyConfig.Value;

    public static string GetConnectionString()
    {
        var connectionString = Environment.GetEnvironmentVariable("connection-string");

        if (string.IsNullOrEmpty(connectionString))
        {
            connectionString = AppSettings.GetSection("PostgreSQL:ConnectionString").Value;
        }

        return connectionString;
    }

    private static readonly Lazy<IConfigurationRoot> LazyConfig = new(() => new ConfigurationBuilder().SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)).AddJsonFile("appsettings.json").Build());
}