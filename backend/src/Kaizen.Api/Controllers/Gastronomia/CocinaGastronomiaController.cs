using Kaizen.Aplicacion.Gastronomia.Dto;
using Kaizen.Aplicacion.Gastronomia.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace Kaizen.Api.Controllers.Gastronomia;

[ApiController]
[Route("api/gastronomia/cocina")]
public class CocinaGastronomiaController : ControllerBase
{
    private readonly ServicioGastronomia _servicio;
    public CocinaGastronomiaController(ServicioGastronomia servicio) => _servicio = servicio;

    [HttpGet("cola")]
    public Task<List<PedidoGastronomiaDto>> Cola(CancellationToken cancellationToken) => _servicio.ObtenerColaCocinaAsync(cancellationToken);
}
