using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using Minio.AspNetCore;
using Notify.Client;
using SimpleFormsService.Configuration;
using SimpleFormsService.Domain.Repositories;
using SimpleFormsService.Persistence;
using SimpleFormsService.Persistence.Repositories;
using SimpleFormsService.Services;
using SimpleFormsService.Services.Abstractions;
using SimpleFormsService.Services.Abstractions.Application;
using SimpleFormsService.Services.Application;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appSettings.json", true, true);

#region DI Configuration

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

builder.Services.AddAuthorization(policies =>
    {
        policies.AddPolicy("GroupAdmin", p =>
        {
            p.RequireClaim("groups", Environment.GetEnvironmentVariable("GROUP_ADMIN_ID"));
        });
    });

builder.Services.AddDbContext<SimpleFormsServiceDbContext>(options => options.UseNpgsql(OpenshiftConfig.Postgres_ConnectionString));
builder.Services.AddHttpContextAccessor();

builder.Services.AddMinio(options =>
    {
        options.Endpoint = OpenshiftConfig.MINIO_EndPoint;
        options.AccessKey = OpenshiftConfig.MINIO_AccessKey;
        options.SecretKey = OpenshiftConfig.MINIO_SecretKey;
    });

builder.Services.Scan(scan => scan.FromAssembliesOf(typeof(IRepositoryBase<>), typeof(RepositoryBase<>))
    .AddClasses(classes => classes.AssignableTo(typeof(IRepositoryBase<>)).Where(type => !type.IsGenericType), false)
    .AsImplementedInterfaces()
    .WithScopedLifetime());

builder.Services.Scan(scan => scan.FromAssembliesOf(typeof(IServiceBase), typeof(ServiceBase))
    .AddClasses(classes => classes.AssignableTo<IServiceBase>(), false)
    .AsImplementedInterfaces()
    .WithScopedLifetime());

builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();
builder.Services.AddScoped<IServiceManager, ServiceManager>();
builder.Services.AddSingleton(x => new NotificationClient(OpenshiftConfig.GCNotify_BaseURL, OpenshiftConfig.GCNotify_ApiKey));

builder.Services.AddCookiePolicy(options =>
    {
        // This lambda determines whether user consent for non-essential cookies is needed for a given request.
        options.CheckConsentNeeded = context => true;
        options.MinimumSameSitePolicy = SameSiteMode.None;
    });

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

builder.Services.AddControllersWithViews(options =>
    {
        var policy = new AuthorizationPolicyBuilder()
            .RequireAuthenticatedUser()
            .Build();
        options.Filters.Add(new AuthorizeFilter(policy));
    }).AddRazorRuntimeCompilation();

#endregion

var app = builder.Build();

#region HTTP Configuration

app.UseForwardedHeaders();
app.UseCookiePolicy();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapControllerRoute(name: "default", pattern: "{controller=Admin}/{action=Index}/{id?}");
    //endpoints.MapRazorPages();
});

#endregion

app.Run();