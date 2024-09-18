using Application.Infrastrucure;
using FT.CQRS;
using FT.CQRS.DependencyInjection;
using FT.CQRS.Decorators;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// A Logging konfigur�l�sa
builder.Logging.ClearProviders();  // Elt�vol�tja az alap�rtelmezett log provider-eket
builder.Logging.AddConsole();      // Hozz�adja a konzol logol�st
builder.Logging.SetMinimumLevel(LogLevel.Debug);  // Be�ll�tja a minimum szintet (Debug)
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IBus, MessageBus>();
builder.Services.AddHandlers();
builder.Services.AddLogging();
builder.Services.AddDbContext<PaymentRequestContext>();
builder.Services.AddTransient<PaymentStatusRepository>();



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
