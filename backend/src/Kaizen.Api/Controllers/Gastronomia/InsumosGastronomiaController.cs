using Kaizen.Aplicacion.Gastronomia.Dto;
using Kaizen.Aplicacion.Gastronomia.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace Kaizen.Api.Controllers.Gastronomia;

[ApiController]
[Route("api/gastronomia/insumos")]
public class InsumosGastronomiaController : ControllerBase
{
    private readonly ServicioGastronomia _servicio;
    public InsumosGastronomiaController(ServicioGastronomia servicio) => _servicio = servicio;

    [HttpGet]
    public Task<List<InsumoGastronomiaDto>> Obtener(CancellationToken cancellationToken) => _servicio.ObtenerInsumosAsync(cancellationToken);

    [HttpPost]
    public Task<InsumoGastronomiaDto> Crear([FromBody] CrearInsumoGastronomiaDto dto, CancellationToken cancellationToken) => _servicio.CrearInsumoAsync(dto, cancellationToken);
}
