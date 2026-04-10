using Kaizen.Aplicacion.Administracion.Interfaces;
using Kaizen.Dominio.Administracion;
using Kaizen.Infraestructura.Persistencia;
using Microsoft.EntityFrameworkCore;

namespace Kaizen.Infraestructura.Administracion;

public class AdministracionRepositorio : IAdministracionRepositorio
{
    private readonly KaizenDbContext _dbContext;

    public AdministracionRepositorio(KaizenDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task RegistrarImpactoAsync(ImpactoComercial impacto, CancellationToken cancellationToken) =>
        _dbContext.ImpactosComerciales.AddAsync(impacto, cancellationToken).AsTask();

    public Task<List<ImpactoComercial>> ObtenerImpactosAsync(CancellationToken cancellationToken) =>
        _dbContext.ImpactosComerciales.AsNoTracking().OrderByDescending(x => x.FechaOperacionUtc).ToListAsync(cancellationToken);
}
