using Kaizen.Aplicacion.Gastronomia.Dto;
using Kaizen.Aplicacion.Gastronomia.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace Kaizen.Api.Controllers.Gastronomia;

[ApiController]
[Route("api/gastronomia/recetas")]
public class RecetasGastronomiaController : ControllerBase
{
    private readonly ServicioGastronomia _servicio;
    public RecetasGastronomiaController(ServicioGastronomia servicio) => _servicio = servicio;

    [HttpGet("producto/{productoId:guid}")]
    public Task<RecetaProductoDto> ObtenerPorProducto(Guid productoId, CancellationToken cancellationToken) =>
        _servicio.ObtenerRecetaPorProductoAsync(productoId, cancellationToken);

    [HttpPost]
    public Task<RecetaProductoDto> Guardar([FromBody] GuardarRecetaProductoDto dto, CancellationToken cancellationToken) =>
        _servicio.GuardarRecetaAsync(dto, cancellationToken);
}
