using Kaizen.Aplicacion.Gastronomia.Dto;
using Kaizen.Aplicacion.Gastronomia.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace Kaizen.Api.Controllers.Gastronomia;

[ApiController]
[Route("api/gastronomia/productos")]
public class ProductosGastronomiaController : ControllerBase
{
    private readonly ServicioGastronomia _servicio;
    public ProductosGastronomiaController(ServicioGastronomia servicio) => _servicio = servicio;

    [HttpGet]
    public Task<List<ProductoGastronomiaDto>> Obtener(CancellationToken cancellationToken) => _servicio.ObtenerProductosAsync(cancellationToken);

    [HttpPost]
    public Task<ProductoGastronomiaDto> Crear([FromBody] CrearProductoGastronomiaDto dto, CancellationToken cancellationToken) => _servicio.CrearProductoAsync(dto, cancellationToken);
}
