using Microsoft.EntityFrameworkCore;
using MiniCoreMultiCliente.MiniCore.Infrastructure.Data;
using MiniCoreMultiCliente.MiniCore.Application.Interfaces;
using MiniCoreMultiCliente.MiniCore.Application.Services;
using MiniCoreMultiCliente.MiniCore.Application.Strategies;
using MiniCoreMultiCliente.MiniCore.Application.Factory;

var builder = WebApplication.CreateBuilder(args);

// Base de datos
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Servicios de aplicación
builder.Services.AddScoped<IComisionService, ComisionService>();
builder.Services.AddScoped<IComisionRepository, ComisionRepository>();
builder.Services.AddScoped<IReglaComisionEstrategia, ReglaComisionEstandar>();
builder.Services.AddScoped<IReglaComisionFactory, ReglaComisionFactory>();


// CORS (esto debe ir antes de Build)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});


// Swagger y controladores
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Swagger solo en desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Middleware
app.UseCors("AllowAll");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
