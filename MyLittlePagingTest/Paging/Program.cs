using Contracts;
using LoggerService;
using Microsoft.AspNetCore.Mvc;
using NLog;
using Paging.Extensions;
using Service;
using Service.Contracts;

LogManager.LoadConfiguration(Path.Combine(
    path1: Directory.GetCurrentDirectory(),
    path2: "./nlog.config"));

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(
        configure: (MvcOptions opts) =>
        {
            opts.RespectBrowserAcceptHeader = true;
            opts.ReturnHttpNotAcceptable = true;
        })
        .AddApplicationPart(typeof(Tlhahiso.AssemblyReference).Assembly);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ILoggerManager, LoggerManager>();
builder.Services.AddSingleton<IWeatherService, WeatherService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
var logger = app.Services.GetRequiredService<ILoggerManager>();
app.ConfigureExceptionHandler(logger);
/*
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
*/

app.UseAuthorization();

app.MapControllers();

app.Run();
