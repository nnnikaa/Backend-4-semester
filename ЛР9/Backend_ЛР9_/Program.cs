using Backend_ЛР9_.Api.Student.Options;
using Microsoft.Extensions.Logging.Console;
using System.Runtime.InteropServices;
using Winton.Extensions.Configuration.Consul;
using Winton.Extensions.Configuration.Consul.Parsers;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();

builder.Services.AddSwaggerGen();
builder.Configuration.Sources.Clear();

builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: false, reloadOnChange: true)  //через JSON-файл
    .AddYamlFile("appsettings.yaml", optional: false, reloadOnChange: true)
    .AddYamlFile($"appsettings.{builder.Environment.EnvironmentName}.yaml", optional: true, reloadOnChange: true)
    .AddConsul("Backend_ЛР9_", options =>
    {
        options.Parser = new SimpleConfigurationParser();
        options.ConsulConfigurationOptions = o => o.Address = new Uri("http://localhost:8500");
    });
    .AddEnvironmentVariables();

var settings = builder.Configuration.GetRequiredSection("StudentApiSettings").Get<StudentApiOptions>();

var password = builder.Configuration.GetRequiredSection("PasswordsSettings:Password").Get<string>(); //через переменную среды

builder.Services.Configure<StudentApiOptions>(builder.Configuration.GetRequiredSection("StudentApiSettings"));

var app = builder.Build();

app.UseSwagger().UseSwaggerUI();


app.UseHttpsRedirection();


app.MapControllers();

app.Run();
