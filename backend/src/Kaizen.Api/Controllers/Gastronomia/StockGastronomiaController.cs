using Kaizen.Aplicacion.Gastronomia.Dto;
using Kaizen.Aplicacion.Gastronomia.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace Kaizen.Api.Controllers.Gastronomia;

[ApiController]
[Route("api/gastronomia/stock")]
public class StockGastronomiaController : ControllerBase
{
    private readonly ServicioGastronomia _servicio;
    public StockGastronomiaController(ServicioGastronomia servicio) => _servicio = servicio;

    [HttpGet("movimientos")]
    public Task<List<MovimientoStockGastronomiaDto>> Movimientos(CancellationToken cancellationToken) => _servicio.ObtenerMovimientosStockAsync(cancellationToken);
}
