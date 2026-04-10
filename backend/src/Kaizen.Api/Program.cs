using Kaizen.Aplicacion.Administracion.Interfaces;
using Kaizen.Aplicacion.Administracion.Servicios;
using Kaizen.Aplicacion.Gimnasio.Interfaces;
using Kaizen.Aplicacion.Gimnasio.Servicios;
using Kaizen.Api.Middlewares;
using Kaizen.Infraestructura.Administracion;
using Kaizen.Infraestructura.Gimnasio;
using Kaizen.Infraestructura.Persistencia;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var cadenaConexion = ObtenerCadenaConexion(builder.Configuration);
builder.Services.AddDbContext<KaizenDbContext>(options => options.UseNpgsql(cadenaConexion));

builder.Services.AddScoped<IGimnasioRepositorio, GimnasioRepositorio>();
builder.Services.AddScoped<IAdministracionRepositorio, AdministracionRepositorio>();
builder.Services.AddScoped<ServicioImpactoAdministrativo>();
builder.Services.AddScoped<ServicioGimnasio>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("frontend", policy =>
        policy.WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<MiddlewareErroresHttp>();
app.UseCors("frontend");
app.UseHttpsRedirection();
app.MapControllers();

app.Run();

static string ObtenerCadenaConexion(IConfiguration configuration)
{
    var cadenaDesdeVariableEntorno = Environment.GetEnvironmentVariable("KAIZEN_DB_CONNECTION");
    if (!string.IsNullOrWhiteSpace(cadenaDesdeVariableEntorno))
    {
        return cadenaDesdeVariableEntorno;
    }

    var cadenaDesdeConfiguracion = configuration.GetConnectionString("KaizenDb");
    if (!string.IsNullOrWhiteSpace(cadenaDesdeConfiguracion))
    {
        return cadenaDesdeConfiguracion;
    }

    throw new InvalidOperationException(
        "No hay cadena de conexión definida. Configurá 'ConnectionStrings:KaizenDb' en appsettings/user-secrets o la variable de entorno 'KAIZEN_DB_CONNECTION'."
    );
}
