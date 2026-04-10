using Kaizen.Dominio.Administracion;

namespace Kaizen.Aplicacion.Administracion.Interfaces;

public interface IAdministracionRepositorio
{
    Task RegistrarImpactoAsync(ImpactoComercial impacto, CancellationToken cancellationToken);
    Task GuardarCambiosAsync(CancellationToken cancellationToken);
}
