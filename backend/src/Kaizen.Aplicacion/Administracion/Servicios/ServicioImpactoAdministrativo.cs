using Kaizen.Aplicacion.Administracion.Interfaces;
using Kaizen.Dominio.Administracion;

namespace Kaizen.Aplicacion.Administracion.Servicios;

public class ServicioImpactoAdministrativo
{
    private readonly IAdministracionRepositorio _repositorio;

    public ServicioImpactoAdministrativo(IAdministracionRepositorio repositorio)
    {
        _repositorio = repositorio;
    }

    public Task RegistrarImpactoMembresiaAsync(Guid membresiaId, Guid socioId, decimal monto, CancellationToken cancellationToken)
    {
        var impacto = new ImpactoComercial
        {
            ModuloOrigen = "gimnasio",
            TipoOperacion = "membresia_generada",
            ReferenciaExterna = $"membresia:{membresiaId}",
            Descripcion = $"Alta/renovación de membresía para socio {socioId}",
            Monto = monto,
            FechaOperacionUtc = DateTime.UtcNow
        };

        return _repositorio.RegistrarImpactoAsync(impacto, cancellationToken);
    }

    public Task<List<ImpactoComercial>> ObtenerImpactosAsync(CancellationToken cancellationToken) =>
        _repositorio.ObtenerImpactosAsync(cancellationToken);
}
