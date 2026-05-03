using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger().UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseDefaultFiles();
app.UseStaticFiles();
IHostEnvironment? env = app.Services.GetService<IHostEnvironment>();    
if (env != null) 
{
    app.UseFileServer(new FileServerOptions()
    {
        FileProvider = new PhysicalFileProvider(
                Path.Combine(env.ContentRootPath, "wwwroot")
                ),

        RequestPath = "/wwwroot",
        EnableDefaultFiles = false


    });

}

app.Run();
