using Serilog;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();


builder.Services.AddSwaggerGen();

builder.Services.AddLogging(l =>
{
    l.ClearProviders();
    l.AddSerilog(new LoggerConfiguration()
        .ReadFrom.Configuration(builder.Configuration)
        .CreateLogger());
}
); 



var app = builder.Build();



app.UseSwagger().UseSwaggerUI();


app.UseHttpsRedirection();


app.MapControllers();

app.Run();
