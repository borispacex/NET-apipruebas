using APIPRUEBAS.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DBAPIContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("cadenaSQL")));

// Para detener el bucle en objetos json
builder.Services.AddControllers().AddJsonOptions(opt =>
{
    opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
// configurando Cors
var misReglasCors = "reglasCors";
builder.Services.AddCors(opt =>
{
    opt.AddPolicy(name: misReglasCors, builder =>
    {
        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// activando Cors
app.UseCors(misReglasCors);

app.UseAuthorization();

app.MapControllers();

app.Run();
