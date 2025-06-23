using Microsoft.EntityFrameworkCore;
using MiniCoreMultiCliente.MiniCore.Infrastructure.Data;
using MiniCoreMultiCliente.MiniCore.Application.Interfaces;
using MiniCoreMultiCliente.MiniCore.Application.Services;

var builder = WebApplication.CreateBuilder(args);

// Base de datos
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Servicios de aplicación
builder.Services.AddScoped<IComisionService, ComisionService>();

// CORS (esto debe ir antes de Build)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontendDev", policy =>
    {
        policy.WithOrigins("http://localhost:3000") // tu frontend local
              .AllowAnyHeader()
              .AllowAnyMethod();
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
app.UseCors("AllowFrontendDev"); 
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
