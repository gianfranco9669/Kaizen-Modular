using Kaizen.Aplicacion.Gimnasio.Interfaces;
using Kaizen.Dominio.Gimnasio;
using Kaizen.Infraestructura.Persistencia;
using Microsoft.EntityFrameworkCore;

namespace Kaizen.Infraestructura.Gimnasio;

public class GimnasioRepositorio : IGimnasioRepositorio
{
    private readonly KaizenDbContext _dbContext;

    public GimnasioRepositorio(KaizenDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<List<Socio>> ObtenerSociosAsync(CancellationToken cancellationToken) =>
        _dbContext.Socios.AsNoTracking().OrderBy(s => s.Apellido).ToListAsync(cancellationToken);

    public Task<Socio?> ObtenerSocioPorIdAsync(Guid socioId, CancellationToken cancellationToken) =>
        _dbContext.Socios
            .Include(s => s.Membresias)
            .ThenInclude(m => m.Plan)
            .FirstOrDefaultAsync(s => s.Id == socioId, cancellationToken);

    public Task AgregarSocioAsync(Socio socio, CancellationToken cancellationToken) => _dbContext.Socios.AddAsync(socio, cancellationToken).AsTask();

    public Task ActualizarSocioAsync(Socio socio, CancellationToken cancellationToken)
    {
        _dbContext.Socios.Update(socio);
        return Task.CompletedTask;
    }

    public Task<List<Plan>> ObtenerPlanesAsync(CancellationToken cancellationToken) =>
        _dbContext.Planes.AsNoTracking().OrderBy(p => p.Nombre).ToListAsync(cancellationToken);

    public Task<Plan?> ObtenerPlanPorIdAsync(Guid planId, CancellationToken cancellationToken) =>
        _dbContext.Planes.FirstOrDefaultAsync(p => p.Id == planId, cancellationToken);

    public Task AgregarPlanAsync(Plan plan, CancellationToken cancellationToken) => _dbContext.Planes.AddAsync(plan, cancellationToken).AsTask();

    public Task ActualizarPlanAsync(Plan plan, CancellationToken cancellationToken)
    {
        _dbContext.Planes.Update(plan);
        return Task.CompletedTask;
    }

    public Task<List<Membresia>> ObtenerMembresiasAsync(CancellationToken cancellationToken) =>
        _dbContext.Membresias.AsNoTracking().OrderByDescending(m => m.FechaInicio).ToListAsync(cancellationToken);

    public Task<Membresia?> ObtenerMembresiaPorIdAsync(Guid membresiaId, CancellationToken cancellationToken) =>
        _dbContext.Membresias.FirstOrDefaultAsync(m => m.Id == membresiaId, cancellationToken);

    public Task AgregarMembresiaAsync(Membresia membresia, CancellationToken cancellationToken) =>
        _dbContext.Membresias.AddAsync(membresia, cancellationToken).AsTask();

    public Task ActualizarMembresiaAsync(Membresia membresia, CancellationToken cancellationToken)
    {
        _dbContext.Membresias.Update(membresia);
        return Task.CompletedTask;
    }

    public Task AgregarRegistroAccesoAsync(RegistroAcceso registroAcceso, CancellationToken cancellationToken) =>
        _dbContext.RegistrosAcceso.AddAsync(registroAcceso, cancellationToken).AsTask();

    public Task GuardarCambiosAsync(CancellationToken cancellationToken) => _dbContext.SaveChangesAsync(cancellationToken);

    public async Task EjecutarEnTransaccionAsync(Func<CancellationToken, Task> operacion, CancellationToken cancellationToken)
    {
        await using var transaccion = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            await operacion(cancellationToken);
            await transaccion.CommitAsync(cancellationToken);
        }
        catch
        {
            await transaccion.RollbackAsync(cancellationToken);
            throw;
        }
    }
}
