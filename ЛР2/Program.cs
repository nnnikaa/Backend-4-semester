using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using System.Xml.Linq;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSwaggerGen();
var app = builder.Build();
app.UseSwagger().UseSwaggerUI();

app.MapGet("/params/queryparams", (int? id, string? name) => {
    return Results.Json(new { CalledMethod = "GET", Params = $"Передаем данные id = {id}, name = {name}" });
    });

app.MapPost("params/body", ([FromBody] HumanBodyRequestContract request) => {
    return Results.Json(new { CalledMethod = "POST", Params = $"Принимаем данные id = {request.Id}, name = {request.Name}" });
});


app.MapPost("params/query", (int? id, string? name, [FromBody] HumanBodyRequestContract request) => {
    return Results.Json(new { CalledMethod = "POST", 
        Params = $"Принимаем данные через тело id = {request.Id}, name = {request.Name}; через query-параметры id = {id}, name = {name}" });
});

app.MapPut("params/putbody", ([FromBody] HumanBodyRequestContract request) => {
    return Results.Json(new { CalledMethod = "PUT", NewId = request.Id, NewName = request.Name, Params= $"Принимаем данные через тело id = {request.Id}, name = {request.Name}" });
});


app.MapPatch("params/patch", ([FromBody] HumanPatchBodyRequestContract request) => {
    return Results.Json(new { CalledMethod = "PATCH", NewId = request.Id, NewName = request.Name, Params = $"Частичное обновление данных id = {request.Id}, name = {request.Name} " });
});




app.Run();
class HumanBodyRequestContract
{
    public required int Id { get; init; }
    public required string Name { get; init; }
}


class HumanPatchBodyRequestContract
{
    public int? Id { get; init; }
    public string? Name { get; init; }
}
