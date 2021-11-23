namespace SimpleFormsService.API.Global
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
            GCNotifyBaseURL = string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("BASE_URL")) ? _config.GetValue<string>("GCNotify:BaseURL") : Environment.GetEnvironmentVariable("BASE_URL");
            GCNotifyTemplateId = string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("TEMPLATE_ID")) ? _config.GetValue<string>("GCNotify:TemplateId") : Environment.GetEnvironmentVariable("TEMPLATE_ID");
            GCNotifyApiKey = string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("API_KEY")) ? _config.GetValue<string>("GCNotify:ApiKey") : Environment.GetEnvironmentVariable("API_KEY");
        }

        public string GCNotifyApiKey { get; set; }
        public string GCNotifyTemplateId { get; set; }
        public string GCNotifyBaseURL { get; set; }
        
    }
}
