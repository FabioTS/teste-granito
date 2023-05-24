using System.Reflection;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

#region Swagger
// Set the comments path for the Swagger JSON and UI.
var apiName = Assembly.GetExecutingAssembly().GetName().Name ?? string.Empty;
var xmlFile = apiName + ".xml";
string xmlPath;
try
{
    var slnPath = System.IO.Directory.GetParent(AppContext.BaseDirectory.Substring(0, AppContext.BaseDirectory.IndexOf(apiName)))?.ToString() ?? string.Empty;
    var apiPath = System.IO.Path.Combine(slnPath, apiName);
    xmlPath = System.IO.Path.Combine(apiPath + "/bin", xmlFile);
}
catch (Exception)
{
    xmlPath = System.IO.Path.Combine(AppContext.BaseDirectory, xmlFile);
}

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Tax Api",
        Version = "v1",
        Description = "Api de taxas para teste da Granito",
        Contact = new OpenApiContact
        {
            Name = "Fábio Trevizolo de Souza",
            Email = "ftrevizolos@gmail.com",
            Url = new Uri("https://github.com/FabioTS"),
        }
    });
    c.IncludeXmlComments(xmlPath);
    c.AddServer(new() { Url = "/taxapi", Description = "nginx rewrite path" });
    c.AddServer(new() { Url = "/", Description = "localhost" });
});
#endregion

var app = builder.Build();

app.UseRewriter(new RewriteOptions().AddRedirect("^$", "swagger"));

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
