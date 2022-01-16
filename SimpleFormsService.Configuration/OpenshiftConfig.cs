using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace SimpleFormsService.Configuration
{
    /// <summary>
    /// Store all custom configurations for the app
    /// e.g. Email config, storage access keys, etc
    /// </summary>
    public static class OpenshiftConfig
    {
        public static IConfigurationRoot SharedSettings => LazyConfig.Value;

        #region Config Properties

        public static string GCNotify_ApiKey => string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("API_KEY")) ? SharedSettings.GetSection("GCNotify:ApiKey").Value : Environment.GetEnvironmentVariable("API_KEY");
        public static string GCNotify_BaseURL => string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("BASE_URL")) ? SharedSettings.GetSection("GCNotify: BaseURL").Value : Environment.GetEnvironmentVariable("BASE_URL");
        public static string GCNotify_TemplateId => string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("TEMPLATE_ID")) ? SharedSettings.GetSection("GCNotify:TemplateId").Value : Environment.GetEnvironmentVariable("TEMPLATE_ID");

        public static string MINIO_EndPoint => string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("MINIO_ENDPOINT")) ? SharedSettings.GetSection("Minio:Endpoint").Value : Environment.GetEnvironmentVariable("MINIO_ENDPOINT");
        public static string MINIO_AccessKey => string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("MINIO_ACCESSKEY")) ? SharedSettings.GetSection("Minio:AccessKey").Value : Environment.GetEnvironmentVariable("MINIO_ACCESSKEY");
        public static string MINIO_SecretKey => string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("MINIO_SECRETKEY")) ? SharedSettings.GetSection("Minio:SecretKey").Value : Environment.GetEnvironmentVariable("MINIO_SECRETKEY");

        public static string Postgres_ConnectionString
        {
            get
            {
                Console.WriteLine("=====INFO: connection-string empty? " + string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("connection-string")) + " ======");
                Console.WriteLine("=====INFO: POSTGRES_CONNECT_STRING empty? " + string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("POSTGRES_CONNECT_STRING")) + " ======");
                // hack to workaround default connection-string environment variable created by OpenShift
                var postgresConnectionString = string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("connection-string")) ? Environment.GetEnvironmentVariable("POSTGRES_CONNECT_STRING") : Environment.GetEnvironmentVariable("connection-string"); 
                return string.IsNullOrWhiteSpace(postgresConnectionString) ? SharedSettings.GetSection("PostgreSQL:ConnectionString").Value : postgresConnectionString;
            }
        }
       
        #endregion

        private static readonly Lazy<IConfigurationRoot> LazyConfig = new(() => new ConfigurationBuilder()
            .SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
            .AddJsonFile("sharedsettings.json").Build());
    }
}
