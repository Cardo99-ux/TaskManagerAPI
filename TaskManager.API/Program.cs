using Microsoft.EntityFrameworkCore;
using TaskManager.API.Data;

var builder = WebApplication.CreateBuilder(args);

// 1. Agregar servicios al contenedor.
builder.Services.AddControllers();

// Agregamos el generador de Swagger (Indispensable para que UseSwaggerUI funcione)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuración de la conexión a MySQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=taskmanager.db"));



var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI(c =>
{
    // Ajustamos la ruta del JSON para el generador estándar de Swagger
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "TaskManager API V1");
    
    // Swagger como la página de inicio:
    c.RoutePrefix = string.Empty; 
});

// Comentamos la redirección HTTPS temporalmente para evitar el error de puertos que vimos en tu terminal
// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.EnsureCreated(); // -- Crea el archivo .db y las tablas si no existen
}

app.Run();