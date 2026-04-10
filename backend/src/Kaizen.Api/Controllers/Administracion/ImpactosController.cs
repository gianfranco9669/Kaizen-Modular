using Kaizen.Aplicacion.Administracion.Dto;
using Kaizen.Aplicacion.Administracion.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace Kaizen.Api.Controllers.Administracion;

[ApiController]
[Route("api/administracion/impactos")]
public class ImpactosController : ControllerBase
{
    private readonly ServicioImpactoAdministrativo _servicio;

    public ImpactosController(ServicioImpactoAdministrativo servicio)
    {
        _servicio = servicio;
    }

    [HttpGet]
    public Task<List<ImpactoComercialDto>> Obtener(CancellationToken cancellationToken) =>
        _servicio.ObtenerImpactosAsync(cancellationToken);
}
