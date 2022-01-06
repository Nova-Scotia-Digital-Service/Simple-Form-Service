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
            options.CallbackPath = string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("CALL_BACK_PATH")) ? builder.Configuration["AzureAd:CallbackPath"] : Environment.GetEnvironmentVariable("CALL_BACK_PATH");
        },
        cookieOptions =>
        {
            cookieOptions.ExpireTimeSpan = TimeSpan.FromMinutes(10);
            cookieOptions.SlidingExpiration = true;
        });

builder.Services.AddCookiePolicy(options =>
{
    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.None;
});

//TODO: authorization
builder.Services.AddAuthorization(policies =>
{
    policies.AddPolicy("GroupAdmin", p =>
    {
        p.RequireClaim("groups", Environment.GetEnvironmentVariable("GROUP_ADMIN_ID"));
    });
});

// Add services to the container.
builder.Services.AddRazorPages(o => { o.Conventions.AuthorizePage("/SubmissionDetail", "GroupAdmin"); }).AddMvcOptions(options =>
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

builder.Services.AddControllers(options =>
{
    var policy = new AuthorizationPolicyBuilder()
    .RequireAuthenticatedUser()
    .Build();
    options.Filters.Add(new AuthorizeFilter(policy));
});

var app = builder.Build();

app.UseForwardedHeaders();
app.UseCookiePolicy();
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
