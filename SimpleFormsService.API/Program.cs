using Microsoft.EntityFrameworkCore;
using SimpleFormsService.API.Configs;
using SimpleFormsService.API.Data;
using SimpleFormsService.API.Services;
using SimpleFormsService.API.Services.Impl;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appSettings.json", optional: true, reloadOnChange: true);

// Read environment variables from OpenShift
OpenshiftConfig openshiftConfig = new(builder.Configuration);

Console.Write("====== INFO: GCNotify templateID is NULL?? " + string.IsNullOrWhiteSpace(openshiftConfig.GCNotify_TemplateId) + "======");
Console.Write("====== INFO: Postgresql connection string is NULL?? " + string.IsNullOrWhiteSpace(openshiftConfig.Postgre_ConnectionString) + "======");
Console.Write("====== INFO: MINIO endpoint - " + openshiftConfig.MINIO_EndPoint + " ======");

builder.Services.AddSingleton(openshiftConfig);
builder.Services.AddTransient<IDocumentService, MinIoFileStorageService>();
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<PostgreSqlContext>(options => options.UseNpgsql(openshiftConfig.Postgre_ConnectionString));


//TODO: Add azure ad integration
var app = builder.Build();

// Configure the HTTP request pipeline.
// TODO: get it working in DEV. turn it on when go to PROD
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


//app.MapGet("/api/admin/{formId}/{submissionId}/view-form",
//(string formId, string submissionId, SimpleFormsServiceImpl secureFormService) => secureFormService.ViewForm(formId, submissionId));

//app.MapGet("/api/admin/{formId}/{submissionId}/view-form", 
//(string formId, string submissionId) => $"The user id is {form-id} and book id is {submission - id}");


/*

//API methods to support the public interface
app.MapPost("/api/public/{form-id}/{submission-id}/upload-file") 
app.MapPost("/api/public/{form-id}/{submission-id}/submit-form") 

//API methods to support the admin interface
app.MapGet("/api/admin/{form-id}/{submission-id}/view-form")
app.MapGet("/api/admin/{form-id}/{submission-id}/view-file/{file-id}")

//API methods to support form configuration, not required for MVP as we can hard code
app.MapPost("/api/config/{form-id}) //create a new form configuration
app.MapGet("/api/config/{form-id}) // get the config for the specified form
app.MapPatch("/api/config/{form-id}) //update the current form's configuration.  Ensure not to change the id (obviously)
 
*/
app.Run();

