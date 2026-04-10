using Kaizen.Aplicacion.Gastronomia.Dto;
using Kaizen.Aplicacion.Gastronomia.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace Kaizen.Api.Controllers.Gastronomia;

[ApiController]
[Route("api/gastronomia/categorias")]
public class CategoriasGastronomiaController : ControllerBase
{
    private readonly ServicioGastronomia _servicio;
    public CategoriasGastronomiaController(ServicioGastronomia servicio) => _servicio = servicio;

    [HttpGet]
    public Task<List<CategoriaGastronomiaDto>> Obtener(CancellationToken cancellationToken) => _servicio.ObtenerCategoriasAsync(cancellationToken);

    [HttpPost]
    public Task<CategoriaGastronomiaDto> Crear([FromBody] CrearCategoriaGastronomiaDto dto, CancellationToken cancellationToken) => _servicio.CrearCategoriaAsync(dto, cancellationToken);
}
