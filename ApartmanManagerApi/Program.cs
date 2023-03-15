
using DataAccessLayer.DbAccess;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Operations;
using DataModelLayer.Models;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Factories;
using ServiceLayer.Factories.Interfaces;
using ServiceLayer.ServiceInterfaces;
using ServiceLayer.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
// Add services to the container.


builder.Services.AddSingleton(builder.Configuration);
builder.Services.AddDbContext<AMDbContext>(options =>
    options.UseNpgsql("name=ConnectionStrings:DefaultConnection"));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddTransient(typeof(IGenericDataAccess<>), typeof(GenericDataAccess<>));
builder.Services.AddTransient(typeof(IResponseModelFactory<>), typeof(ResponseModelFactory<>));
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddTransient<IRoomDataAccess, RoomDataAccess>();
builder.Services.AddTransient<IRoomService, RoomService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
