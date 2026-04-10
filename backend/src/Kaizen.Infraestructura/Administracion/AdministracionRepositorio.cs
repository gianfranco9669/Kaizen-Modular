using Kaizen.Aplicacion.Administracion.Interfaces;
using Kaizen.Dominio.Administracion;
using Kaizen.Infraestructura.Persistencia;

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

    public Task GuardarCambiosAsync(CancellationToken cancellationToken) => _dbContext.SaveChangesAsync(cancellationToken);
}
