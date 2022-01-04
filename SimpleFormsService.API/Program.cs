using Microsoft.EntityFrameworkCore;
using Minio.AspNetCore;
using SimpleFormsService.API.Middleware;
using SimpleFormsService.Configuration;
using SimpleFormsService.Domain.Repositories;
using SimpleFormsService.Persistence;
using SimpleFormsService.Persistence.Repositories;
using SimpleFormsService.Services;
using SimpleFormsService.Services.Abstractions;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appSettings.json", optional: true, reloadOnChange: true);

Console.Write("====== INFO: GCNotify templateID is NULL?? " + string.IsNullOrWhiteSpace(OpenshiftConfig.GCNotify_TemplateId) + "======");
Console.Write("====== INFO: Postgresql connection string is NULL?? " + string.IsNullOrWhiteSpace(OpenshiftConfig.Postgres_ConnectionString + "======"));
Console.Write("====== INFO: MINIO endpoint - " + OpenshiftConfig.MINIO_EndPoint + " ======");

builder.Services.AddControllers()
    .AddApplicationPart(typeof(SimpleFormsService.Presentation.AssemblyReference).Assembly); // add controllers from the SimpleFormsService.Presentation assembly to the container

builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<SimpleFormsServiceDbContext>(options => options.UseNpgsql(OpenshiftConfig.Postgres_ConnectionString));
builder.Services.AddHttpContextAccessor();

builder.Services.AddMinio(options =>
{
    options.Endpoint = OpenshiftConfig.MINIO_EndPoint;
    options.AccessKey = OpenshiftConfig.MINIO_AccessKey;
    options.SecretKey = OpenshiftConfig.MINIO_SecretKey;
});

builder.Services.AddScoped<IServiceManager, ServiceManager>();
builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();
builder.Services.AddTransient<ExceptionHandlingMiddleware>();

//TODO: Add azure ad integration

var app = builder.Build();

// TODO: get it working in DEV. turn it on when go to PROD
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


//app.MapGet("/api/admin/{templateId}/{submissionId}/view-form",
//(string formId, string submissionId, SimpleFormsServiceImpl secureFormService) => secureFormService.ViewForm(formId, submissionId));

//app.MapGet("/api/admin/{templateId}/{submissionId}/view-form", 
//(string formId, string submissionId) => $"The user id is {form-id} and book id is {submission - id}");


/*

//API methods to support the public interface
app.MapPost("/api/public/{templateId}/{submission-id}/upload-file") 
app.MapPost("/api/public/{templateId}/{submission-id}/submit-form") 

//API methods to support the admin interface
app.MapGet("/api/admin/{templateId}/{submission-id}/view-form")
app.MapGet("/api/admin/{templateId}/{submission-id}/view-file/{file-id}")

//API methods to support form configuration, not required for MVP as we can hard code
app.MapPost("/api/config/{templateId}) //create a new form configuration
app.MapGet("/api/config/{templateId}) // get the config for the specified form
app.MapPatch("/api/config/{templateId}) //update the current form's configuration.  Ensure not to change the id (obviously)
 
*/
app.Run();

