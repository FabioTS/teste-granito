using System.Reflection;
using CalculatorApi.Api.Services;
using CalculatorApi.Domain.Handlers;
using CalculatorApi.Domain.Services;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(opts =>
{
    opts.JsonSerializerOptions.WriteIndented = true;
    opts.JsonSerializerOptions.AllowTrailingCommas = true;
    opts.JsonSerializerOptions.ReadCommentHandling = System.Text.Json.JsonCommentHandling.Skip;
});

#region Services
builder.Services.AddScoped<ITaxService, TaxService>();
#endregion

#region Handlers
builder.Services.AddTransient<TaxCalculatorHandler, TaxCalculatorHandler>();
#endregion

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
        Title = "Calculator Api",
        Version = "v1",
        Description = "Api de calculo para teste da Granito",
        Contact = new OpenApiContact
        {
            Name = "Fábio Trevizolo de Souza",
            Email = "ftrevizolos@gmail.com",
            Url = new Uri("https://github.com/FabioTS"),
        }
    });
    c.IncludeXmlComments(xmlPath);
});
#endregion

var app = builder.Build();

app.UseRewriter(new RewriteOptions().AddRedirect("^$", "swagger"));

app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()
);

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
