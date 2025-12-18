using System.Data;
using System.Reflection;
using AirlineBookingSystem.Bookings.Core.Repositories;
using AirlineBookingSystem.Bookings.Infrastructure.Repositories;
using Microsoft.Data.SqlClient;
using AirlineBookingSystem.Bookings.Application.Handlers;
using MassTransit;
using AirlineBookingSystem.BuildingBlocks.Common;
using AirlineBookingSystem.Bookings.Application.Consumers;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
var assemblies = new Assembly[]
{
    Assembly.GetExecutingAssembly(),
    typeof(CreateBookingHandler).Assembly,
    typeof(GetBookingHandler).Assembly
};
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assemblies));
builder.Services.AddScoped<IBookingRepository, BookingRepository>();

//Sql
//builder.Services.AddScoped<IDbConnection>(sp => new SqlConnection(builder.Configuration.GetConnectionString("Default")));

//Redis
var redisConfiguration = builder.Configuration["CacheSettings:ConnectionString"];
var redis = ConnectionMultiplexer.Connect(redisConfiguration!);
 builder.Services.AddSingleton<IConnectionMultiplexer>(redis);

//MassTransit 
builder.Services.AddMassTransit(config =>
{
    //Mark this as consumer
    config.AddConsumer<NotificationEventConsumer>();

    config.UsingRabbitMq((ct, cfg) =>
    {
        cfg.Host(builder.Configuration["EventBusSettings:HostAddress"]);
        cfg.ReceiveEndpoint(EventBusConstant.NotificationSentQueue, c =>
        {
            c.ConfigureConsumer<NotificationEventConsumer>(ct);
        });
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
