using Kaizen.Aplicacion.Administracion.Dto;
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


    public Task RegistrarImpactoOperacionAsync(string moduloOrigen, string tipoOperacion, string referenciaExterna, decimal monto, string descripcion, CancellationToken cancellationToken)
    {
        var impacto = new ImpactoComercial
        {
            ModuloOrigen = moduloOrigen,
            TipoOperacion = tipoOperacion,
            ReferenciaExterna = referenciaExterna,
            Descripcion = descripcion,
            Monto = monto,
            FechaOperacionUtc = DateTime.UtcNow
        };

        return _repositorio.RegistrarImpactoAsync(impacto, cancellationToken);
    }

    public async Task<List<ImpactoComercialDto>> ObtenerImpactosAsync(CancellationToken cancellationToken)
    {
        var impactos = await _repositorio.ObtenerImpactosAsync(cancellationToken);
        return impactos.Select(i => new ImpactoComercialDto(
            i.Id,
            i.ModuloOrigen,
            i.TipoOperacion,
            i.ReferenciaExterna,
            i.Descripcion,
            i.Monto,
            i.FechaOperacionUtc
        )).ToList();
    }
}
