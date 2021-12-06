namespace SimpleFormsService.API.Configs
{
    /// <summary>
    /// Store all custom configurations for the app
    /// e.g. Email config, storage access keys, etc
    /// </summary>
    public class OpenshiftConfig
    {
        private readonly IConfiguration _config;
        public OpenshiftConfig(IConfiguration config) 
        {
            _config = config;

            //read env variables for GCNotify
            GCNotify_BaseURL = string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("BASE_URL")) ? _config.GetValue<string>("GCNotify:BaseURL") : Environment.GetEnvironmentVariable("BASE_URL");
            GCNotify_TemplateId = string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("TEMPLATE_ID")) ? _config.GetValue<string>("GCNotify:TemplateId") : Environment.GetEnvironmentVariable("TEMPLATE_ID");
            GCNotify_ApiKey = string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("API_KEY")) ? _config.GetValue<string>("GCNotify:ApiKey") : Environment.GetEnvironmentVariable("API_KEY");

            //read env variables for Minio
            MINIO_EndPoint = string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("MINIO_ENDPOINT")) ? _config.GetValue<string>("Minio:Endpoint") : Environment.GetEnvironmentVariable("MINIO_ENDPOINT");
            MINIO_AccessKey = string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("MINIO_ACCESSKEY")) ? _config.GetValue<string>("Minio:AccessKey") : Environment.GetEnvironmentVariable("MINIO_ACCESSKEY");
            MINIO_SecretKey = string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("MINIO_SECRETKEY")) ? _config.GetValue<string>("Minio:SecretKey") : Environment.GetEnvironmentVariable("MINIO_SECRETKEY");
           
            //read postgresql connection string.. hack to workaround default connection-string environment variable created by OpenShift. 
            Postgre_ConnectionString = string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("connection-string")) ? Environment.GetEnvironmentVariable("POSTGRES_CONNECT_STRING") : Environment.GetEnvironmentVariable("connection-string");
            Postgre_ConnectionString = string.IsNullOrWhiteSpace(Postgre_ConnectionString) ? _config.GetValue<string>("PostgreSQL:ConnectionString") : Postgre_ConnectionString;



            //read env variables for Azure AD
            AzureAD_ClientId = string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("CLIENT_ID")) ? _config.GetValue<string>("AzureAd:ClientId") : Environment.GetEnvironmentVariable("CLIENT_ID");
            AzureAD_TenantId = string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("TENANT_ID")) ? _config.GetValue<string>("AzureAd:TenantId") : Environment.GetEnvironmentVariable("TENANT_ID");
            AzureAD_Instance = string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("AZURE_AD_INSTANCE")) ? _config.GetValue<string>("AzureAd:Instance") : Environment.GetEnvironmentVariable("AZURE_AD_INSTANCE");
            AzureAD_CallBackPath = string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("CALL_BACK_PATH")) ? _config.GetValue<string>("AzureAd:CallbackPath") : Environment.GetEnvironmentVariable("CALL_BACK_PATH");
        }

        public string GCNotify_ApiKey { get; set; }
        public string GCNotify_TemplateId { get; set; }
        public string GCNotify_BaseURL { get; set; }
        public string MINIO_EndPoint { get; set; }
        public string MINIO_AccessKey { get; set; }
        public string MINIO_SecretKey { get; set; }
        public string Postgre_ConnectionString { get; set; }
        public string AzureAD_ClientId { get; set; }
        public string AzureAD_TenantId { get; set; }
        public string AzureAD_Instance { get; set; }
        public string AzureAD_CallBackPath { get; set; }
        
    }
}
