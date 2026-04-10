using Kaizen.Aplicacion.Gimnasio.Dto;
using Kaizen.Aplicacion.Gimnasio.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace Kaizen.Api.Controllers;

[ApiController]
[Route("api/gimnasio/membresias")]
public class MembresiasController : ControllerBase
{
    private readonly ServicioGimnasio _servicio;

    public MembresiasController(ServicioGimnasio servicio)
    {
        _servicio = servicio;
    }

    [HttpGet]
    public Task<List<MembresiaDto>> Obtener(CancellationToken cancellationToken) =>
        _servicio.ObtenerMembresiasAsync(cancellationToken);

    [HttpPost]
    public Task<MembresiaDto> Crear([FromBody] CrearMembresiaDto dto, CancellationToken cancellationToken) =>
        _servicio.CrearMembresiaAsync(dto, cancellationToken);

    [HttpPost("validar-acceso")]
    public Task<ResultadoAccesoDto> ValidarAcceso([FromBody] ValidarAccesoDto dto, CancellationToken cancellationToken) =>
        _servicio.ValidarYRegistrarAccesoAsync(dto.SocioId, cancellationToken);
}
