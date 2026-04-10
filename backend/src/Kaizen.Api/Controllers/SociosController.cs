using Kaizen.Aplicacion.Gimnasio.Dto;
using Kaizen.Aplicacion.Gimnasio.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace Kaizen.Api.Controllers;

[ApiController]
[Route("api/gimnasio/socios")]
public class SociosController : ControllerBase
{
    private readonly ServicioGimnasio _servicio;

    public SociosController(ServicioGimnasio servicio)
    {
        _servicio = servicio;
    }

    [HttpGet]
    public Task<List<SocioDto>> Obtener(CancellationToken cancellationToken) =>
        _servicio.ObtenerSociosAsync(cancellationToken);

    [HttpPost]
    public Task<SocioDto> Crear([FromBody] CrearSocioDto dto, CancellationToken cancellationToken) =>
        _servicio.CrearSocioAsync(dto, cancellationToken);

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<SocioDto>> Actualizar(Guid id, [FromBody] CrearSocioDto dto, CancellationToken cancellationToken)
    {
        var actualizado = await _servicio.ActualizarSocioAsync(id, dto, cancellationToken);
        return actualizado is null ? NotFound() : Ok(actualizado);
    }
}
