using OnionAPI.Persistence;
using OnionAPI.Application; // Project Reference vermeden cagirabiliyoruz
using OnionAPI.Mapper;
using OnionAPI.Application.Exceptions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Environment settings
var env = builder.Environment;
builder.Configuration
    .SetBasePath(env.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: false)
    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddApplication(); // b�ylece ba��ml�l�k azaltm�� oluyoruz
builder.Services.AddCustomMapper();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Exception Middleware
app.ConfigureExceptionHandlingMiddleware();

app.UseAuthorization();

app.MapControllers();

app.Run();
