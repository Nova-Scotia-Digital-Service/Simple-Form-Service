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
        }

        public string GCNotify_ApiKey { get; set; }
        public string GCNotify_TemplateId { get; set; }
        public string GCNotify_BaseURL { get; set; }
        public string MINIO_EndPoint { get; set; }
        public string MINIO_AccessKey { get; set; }
        public string MINIO_SecretKey { get; set; }
        
    }
}
