using Kaizen.Aplicacion.Gastronomia.Dto;
using Kaizen.Aplicacion.Gastronomia.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace Kaizen.Api.Controllers.Gastronomia;

[ApiController]
[Route("api/gastronomia/dashboard")]
public class GastronomiaDashboardController : ControllerBase
{
    private readonly ServicioGastronomia _servicio;
    public GastronomiaDashboardController(ServicioGastronomia servicio) => _servicio = servicio;

    [HttpGet]
    public Task<DashboardGastronomiaDto> Obtener(CancellationToken cancellationToken) => _servicio.ObtenerDashboardAsync(cancellationToken);
}
