using Microsoft.AspNetCore.Rewrite;
using System.Net.Mime;
using static System.Net.Mime.MediaTypeNames;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseSwagger().UseSwaggerUI();

app.MapGet("/html", () =>
{
    var stream = File.ReadAllBytes(Path.Combine("wwwroot", "index.html"));
    return Results.File(stream, "text/html", "html.html");

});

app.MapGet("/txt", () =>
{
    var stream = File.ReadAllBytes(Path.Combine("wwwroot", "Текстовый документ.txt"));
    return Results.File(stream, "text/plain", "Текстовый-файл.txt");
});

app.MapGet("/json", () =>
{
    var stream = File.ReadAllBytes(Path.Combine("wwwroot", "htmlpage.json"));
    return Results.File(stream, "application/json", "json-файл.json");
});

app.MapGet("/xml", () =>
{
    var stream = File.ReadAllBytes(Path.Combine("wwwroot","Class.xml"));
    return Results.File(stream, "application/xml", "xml-файл.xml");
});


app.MapGet("/csv", () =>
{
    var stream = File.ReadAllBytes(Path.Combine("wwwroot", "Class.csv"));
    return Results.File(stream, "text/csv", "csv-файл.csv");
});

app.MapGet("/bin", () =>
{
    var stream = File.ReadAllBytes(Path.Combine("wwwroot", "page.bin"));
    return Results.File(stream, "application/octet-stream", "bin-файл.bin");

});

app.MapGet("/png", () => 
{
    var stream = File.ReadAllBytes(Path.Combine("wwwroot", "Снимок экрана.png"));
    return Results.File(stream, "image/png", "картинка.png");
});


app.MapGet("/pdf", () =>
{
    var stream = File.ReadAllBytes(Path.Combine("wwwroot", "pdf_ru.pdf"));
    return Results.File(stream, "application/pdf", "pdf-файл.pdf");
});

app.MapGet("/redirect", () =>
{
    return Results.Redirect("/302/home", permanent:false);
});

app.MapGet("/302/home", () => "Hello, home!");
app.Run();
