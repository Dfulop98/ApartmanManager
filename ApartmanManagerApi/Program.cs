
using ApartmanManagerApi.Util;
using DataAccessLayer.DbAccess;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Operations;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.ServiceInterfaces;
using ServiceLayer.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
// Add services to the container.


builder.Services.AddSingleton(builder.Configuration);
builder.Services.AddDbContext<AMDbContext>(options =>
    //options.UseNpgsql("name=ConnectionStrings:DefaultConnection"));
    //options.UseInMemoryDatabase("ApartmanDB"));
    options.UseSqlServer(builder.Configuration.GetConnectionString("MsSqlConnection")));
builder.Services.AddCors(options =>
{
    options.AddPolicy("MyCorsPolicy", builder =>
    {
        builder.WithOrigins("http://localhost:3000") // Nextjs alkalmazásod URL-je
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient(typeof(IGenericDataAccess<>), typeof(GenericDataAccess<>));
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddTransient<IImagesDataAccess, ImagesDataAccess>();
builder.Services.AddTransient<IReservationDataAccess, ReservationDataAccess>();
builder.Services.AddTransient<IRoomService, RoomService>();
builder.Services.AddTransient<IGuestService, GuestService>();
builder.Services.AddTransient<IReservationService, ReservationService>();
builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddTransient<IImagesService, ImagesService>();

builder.Services.Configure<StmpConfig>(builder.Configuration.GetSection("stmp"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("MyCorsPolicy");
app.UseAuthorization();

app.MapControllers();

app.Run();

