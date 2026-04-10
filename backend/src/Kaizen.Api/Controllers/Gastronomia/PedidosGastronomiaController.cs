using Kaizen.Aplicacion.Gastronomia.Dto;
using Kaizen.Aplicacion.Gastronomia.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace Kaizen.Api.Controllers.Gastronomia;

[ApiController]
[Route("api/gastronomia/pedidos")]
public class PedidosGastronomiaController : ControllerBase
{
    private readonly ServicioGastronomia _servicio;
    public PedidosGastronomiaController(ServicioGastronomia servicio) => _servicio = servicio;

    [HttpGet]
    public Task<List<PedidoGastronomiaDto>> Obtener(CancellationToken cancellationToken) => _servicio.ObtenerPedidosAsync(cancellationToken);

    [HttpPost]
    public Task<PedidoGastronomiaDto> Crear([FromBody] CrearPedidoGastronomiaDto dto, CancellationToken cancellationToken) => _servicio.CrearPedidoAsync(dto, cancellationToken);

    [HttpPut("{pedidoId:guid}/estado")]
    public async Task<ActionResult<PedidoGastronomiaDto>> CambiarEstado(Guid pedidoId, [FromBody] CambiarEstadoPedidoGastronomiaDto dto, CancellationToken cancellationToken)
    {
        var pedido = await _servicio.CambiarEstadoPedidoAsync(pedidoId, dto, cancellationToken);
        return pedido is null ? NotFound() : Ok(pedido);
    }
}
