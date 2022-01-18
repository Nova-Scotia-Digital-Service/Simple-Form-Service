using Microsoft.EntityFrameworkCore;
using Minio.AspNetCore;
using Notify.Client;
using SimpleFormsService.API.Middleware;
using SimpleFormsService.Configuration;
using SimpleFormsService.Domain.Repositories;
using SimpleFormsService.Persistence;
using SimpleFormsService.Persistence.Repositories;
using SimpleFormsService.Services;
using SimpleFormsService.Services.Abstractions;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appSettings.json", optional: true, reloadOnChange: true);

builder.Services.AddControllers()
    .AddApplicationPart(typeof(SimpleFormsService.Presentation.AssemblyReference).Assembly); // add controllers from the SimpleFormsService.Presentation assembly to the container

builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<SimpleFormsServiceDbContext>(options => options.UseNpgsql(OpenshiftConfig.Postgres_ConnectionString));

builder.Services.AddMinio(options =>
{
    options.Endpoint = OpenshiftConfig.MINIO_EndPoint;
    options.AccessKey = OpenshiftConfig.MINIO_AccessKey;
    options.SecretKey = OpenshiftConfig.MINIO_SecretKey;
});

builder.Services.AddHttpContextAccessor();

builder.Services.Scan(scan => scan.FromAssembliesOf(typeof(IRepositoryBase<>), typeof(RepositoryBase<>))
    .AddClasses(classes => classes.AssignableTo(typeof(IRepositoryBase<>)).Where(type => !type.IsGenericType), false)
    .AsImplementedInterfaces()
    .WithScopedLifetime());

builder.Services.Scan(scan => scan.FromAssembliesOf(typeof(IServiceBase), typeof(ServiceBase))
    .AddClasses(classes => classes.AssignableTo<IServiceBase>(), false)
    .AsImplementedInterfaces()
    .WithScopedLifetime());

builder.Services.AddScoped<IServiceManager, ServiceManager>();
builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();
builder.Services.AddSingleton(x => new NotificationClient(OpenshiftConfig.GCNotify_BaseURL, OpenshiftConfig.GCNotify_ApiKey));

builder.Services.AddTransient<ExceptionHandlingMiddleware>();

//TODO: Add azure ad integration

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();