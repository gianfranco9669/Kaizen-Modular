using Kaizen.Aplicacion.Administracion.Interfaces;
using Kaizen.Aplicacion.Administracion.Servicios;
using Kaizen.Aplicacion.Gimnasio.Interfaces;
using Kaizen.Aplicacion.Gimnasio.Servicios;
using Kaizen.Infraestructura.Administracion;
using Kaizen.Infraestructura.Gimnasio;
using Kaizen.Infraestructura.Persistencia;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<KaizenDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("KaizenDb")));

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

app.UseCors("frontend");
app.UseHttpsRedirection();
app.MapControllers();

app.Run();
