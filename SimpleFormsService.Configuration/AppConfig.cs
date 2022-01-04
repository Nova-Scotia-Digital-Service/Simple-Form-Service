using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace SimpleFormsService.Configuration;

public static class AppConfig
{
    public static IConfigurationRoot SharedSettings => LazyConfig.Value;

    public static string Postgres_ConnectionString => string.IsNullOrEmpty(Environment.GetEnvironmentVariable("connection-string")) ? SharedSettings.GetSection("PostgreSQL:ConnectionString").Value : Environment.GetEnvironmentVariable("connection-string");

    private static readonly Lazy<IConfigurationRoot> LazyConfig = new(() => new ConfigurationBuilder()
        .SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
        .AddJsonFile("sharedsettings.json").Build());
}