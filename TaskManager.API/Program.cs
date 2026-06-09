using Microsoft.EntityFrameworkCore;
using TaskManager.API.Data;

var builder = WebApplication.CreateBuilder(args);

// 1. Agregar servicios al contenedor.
builder.Services.AddControllers();

// Agregamos el generador de Swagger (Indispensable para que UseSwaggerUI funcione)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuración de la conexión a MySQL
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

var app = builder.Build();

// 2. Configurar el pipeline de solicitudes HTTP.

// Forzamos a que Swagger se ejecute siempre para pruebas locales
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    // Ajustamos la ruta del JSON para el generador estándar de Swagger
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "TaskManager API V1");
    
    // Esto hace que Swagger sea la página de inicio (http://localhost:5283/)
    c.RoutePrefix = string.Empty; 
});

// Comentamos la redirección HTTPS temporalmente para evitar el error de puertos que vimos en tu terminal
// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();