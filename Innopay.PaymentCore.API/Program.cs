using Application.Infrastrucure;
using FT.CQRS;
using FT.CQRS.DependencyInjection;
using FT.CQRS.Decorators;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// A Logging konfigurálása
builder.Logging.ClearProviders();  // Eltávolítja az alapértelmezett log provider-eket
builder.Logging.AddConsole();      // Hozzáadja a konzol logolást
builder.Logging.SetMinimumLevel(LogLevel.Debug);  // Beállítja a minimum szintet (Debug)
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
