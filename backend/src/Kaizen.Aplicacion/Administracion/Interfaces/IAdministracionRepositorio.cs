using Kaizen.Dominio.Administracion;

namespace Kaizen.Aplicacion.Administracion.Interfaces;

public interface IAdministracionRepositorio
{
    Task RegistrarImpactoAsync(ImpactoComercial impacto, CancellationToken cancellationToken);
    Task<List<ImpactoComercial>> ObtenerImpactosAsync(CancellationToken cancellationToken);
}
