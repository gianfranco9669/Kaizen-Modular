using Kaizen.Aplicacion.Gimnasio.Dto;
using Kaizen.Aplicacion.Gimnasio.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace Kaizen.Api.Controllers;

[ApiController]
[Route("api/gimnasio/planes")]
public class PlanesController : ControllerBase
{
    private readonly ServicioGimnasio _servicio;

    public PlanesController(ServicioGimnasio servicio)
    {
        _servicio = servicio;
    }

    [HttpGet]
    public Task<List<PlanDto>> Obtener(CancellationToken cancellationToken) =>
        _servicio.ObtenerPlanesAsync(cancellationToken);

    [HttpPost]
    public Task<PlanDto> Crear([FromBody] CrearPlanDto dto, CancellationToken cancellationToken) =>
        _servicio.CrearPlanAsync(dto, cancellationToken);

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<PlanDto>> Actualizar(Guid id, [FromBody] CrearPlanDto dto, CancellationToken cancellationToken)
    {
        var actualizado = await _servicio.ActualizarPlanAsync(id, dto, cancellationToken);
        return actualizado is null ? NotFound() : Ok(actualizado);
    }
}
