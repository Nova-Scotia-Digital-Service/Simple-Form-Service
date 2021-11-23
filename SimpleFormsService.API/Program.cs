using SimpleFormsService.API.Global;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appSettings.json", optional: true, reloadOnChange: true);

// Read environment variables from OpenShift
OpenshiftConfig openshiftConfig = new(builder.Configuration);

Console.Write("====== INFO: Openshift config is NULL?? " + string.IsNullOrWhiteSpace(openshiftConfig.GCNotifyTemplateId) + "======");

builder.Services.AddSingleton(openshiftConfig);
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

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

