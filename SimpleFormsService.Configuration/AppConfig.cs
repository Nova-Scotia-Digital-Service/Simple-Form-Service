using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace SimpleFormsService.Configuration;

public static class AppConfig
{
    public static IConfigurationRoot SharedSettings => LazyConfig.Value;

    public static string Postgres_ConnectionString => Environment.GetEnvironmentVariable("POSTGRES_CONNECT_STRING");

    private static readonly Lazy<IConfigurationRoot> LazyConfig = new(() => new ConfigurationBuilder()
        .SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
        .AddJsonFile("sharedsettings.json").Build());
}
