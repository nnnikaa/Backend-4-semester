using Backend_ЛР6_Воробьева_В.Д._241_333.Api.Student.Repositories;
using Backend_ЛР6_Воробьева_В.Д._241_333.Api.Student.Services;

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
