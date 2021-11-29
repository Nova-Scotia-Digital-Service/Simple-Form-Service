using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appSettings.json", optional: true, reloadOnChange: true);

//TODO: read azure ad from API
builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
        .AddMicrosoftIdentityWebApp(options =>
        {
            options.ClientId = string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("CLIENT_ID")) ? builder.Configuration["AzureAd:ClientId"] : Environment.GetEnvironmentVariable("CLIENT_ID"); 
            options.TenantId = string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("TENANT_ID")) ? builder.Configuration["AzureAd:TenantId"] : Environment.GetEnvironmentVariable("TENANT_ID");
            options.Instance = string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("AZURE_AD_INSTANCE")) ? builder.Configuration["AzureAd:Instance"] : Environment.GetEnvironmentVariable("AZURE_AD_INSTANCE");
            options.CallbackPath = string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("CALL_BACK_PATHd")) ? builder.Configuration["AzureAd:CallbackPath"] : Environment.GetEnvironmentVariable("CALL_BACK_PATH");
        },
        cookieOptions =>
        {
            cookieOptions.ExpireTimeSpan = TimeSpan.FromMinutes(10);
            cookieOptions.SlidingExpiration = true;
        });

// Add services to the container.
builder.Services.AddRazorPages().AddMvcOptions(options =>
{
    var policy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
    options.Filters.Add(new AuthorizeFilter(policy));
}).AddMicrosoftIdentityUI(); 

builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders =
        ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
    options.KnownNetworks.Clear();
    options.KnownProxies.Clear();
});

var app = builder.Build();

app.UseForwardedHeaders();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
