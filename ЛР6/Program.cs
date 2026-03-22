using Backend_ËÐ6_.Api.Student.Repositories;
using Backend_ËÐ6_.Api.Student.Services;

var builder = WebApplication.CreateBuilder(args);




builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IStudentRepository, StudentRepository>();
builder.Services.AddSingleton<IStudentService, StudentService>();


var app = builder.Build();

app.UseSwagger().UseSwaggerUI();
app.UseHttpsRedirection();


app.MapControllers();

app.Run();
