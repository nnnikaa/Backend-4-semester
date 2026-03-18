using Backend_ЛР7_Воробьева_В.Д._241_333;
using System.Diagnostics;
using System.Net.Mime;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddSingleton<BlockPathMiddleware>();
builder.Services.AddSingleton<RequestTraceMiddleware>();
builder.Services.AddSingleton<EndpointTimingMiddleware>();

var app = builder.Build();


app.UseHttpsRedirection();



app.UseMiddleware<BlockPathMiddleware>();
app.UseMiddleware<RequestTraceMiddleware>();
app.UseMiddleware<EndpointTimingMiddleware>();


app.MapGet("/ping", () =>{

    return Results.Content("pong", MediaTypeNames.Text.Plain);
});

app.MapGet("/trace", (HttpContext context) => {
    return context.Items["TraceId"];
});

app.MapGet("/error", () => { 
    throw new Exception();
});

app.Run();
