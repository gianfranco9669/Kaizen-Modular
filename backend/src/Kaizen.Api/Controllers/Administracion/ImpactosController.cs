using Kaizen.Aplicacion.Administracion.Servicios;
using Kaizen.Dominio.Administracion;
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
    public Task<List<ImpactoComercial>> Obtener(CancellationToken cancellationToken) =>
        _servicio.ObtenerImpactosAsync(cancellationToken);
}
