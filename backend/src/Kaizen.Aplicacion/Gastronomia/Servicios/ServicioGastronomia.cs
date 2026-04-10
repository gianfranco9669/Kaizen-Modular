using Kaizen.Aplicacion.Administracion.Servicios;
using Kaizen.Aplicacion.Compartido;
using Kaizen.Aplicacion.Gastronomia.Dto;
using Kaizen.Aplicacion.Gastronomia.Interfaces;
using Kaizen.Dominio.Gastronomia;

namespace Kaizen.Aplicacion.Gastronomia.Servicios;

public class ServicioGastronomia
{
    private static readonly HashSet<string> EstadosPedidoValidos = ["nuevo", "en_preparacion", "listo", "entregado", "cancelado"];
    private static readonly HashSet<string> CanalesValidos = ["salon", "mostrador", "delivery"];

    private readonly IGastronomiaRepositorio _repositorio;
    private readonly ServicioImpactoAdministrativo _servicioImpacto;

    public ServicioGastronomia(IGastronomiaRepositorio repositorio, ServicioImpactoAdministrativo servicioImpacto)
    {
        _repositorio = repositorio;
        _servicioImpacto = servicioImpacto;
    }

    public async Task<DashboardGastronomiaDto> ObtenerDashboardAsync(CancellationToken cancellationToken)
    {
        var categorias = await _repositorio.ObtenerCategoriasAsync(cancellationToken);
        var productos = await _repositorio.ObtenerProductosAsync(cancellationToken);
        var insumos = await _repositorio.ObtenerInsumosAsync(cancellationToken);
        var pedidos = await _repositorio.ObtenerPedidosAsync(cancellationToken);
        var hoy = DateTime.UtcNow.Date;

        return new DashboardGastronomiaDto(
            categorias.Count,
            productos.Count,
            insumos.Count,
            pedidos.Count(p => p.Estado is "nuevo" or "en_preparacion" or "listo"),
            pedidos.Where(p => p.Estado == "entregado" && p.FechaPedidoUtc.Date == hoy).Sum(x => x.Total),
            insumos.Count(i => i.StockActual <= i.StockMinimo)
        );
    }

    public async Task<List<CategoriaGastronomiaDto>> ObtenerCategoriasAsync(CancellationToken cancellationToken) =>
        (await _repositorio.ObtenerCategoriasAsync(cancellationToken))
        .Select(c => new CategoriaGastronomiaDto(c.Id, c.Nombre, c.Descripcion, c.Activa)).ToList();

    public async Task<CategoriaGastronomiaDto> CrearCategoriaAsync(CrearCategoriaGastronomiaDto dto, CancellationToken cancellationToken)
    {
        var categoria = new CategoriaGastronomia
        {
            Nombre = ValidadorEntrada.Requerido(dto.Nombre, nameof(dto.Nombre)),
            Descripcion = (dto.Descripcion ?? string.Empty).Trim()
        };
        await _repositorio.AgregarCategoriaAsync(categoria, cancellationToken);
        await _repositorio.GuardarCambiosAsync(cancellationToken);
        return new CategoriaGastronomiaDto(categoria.Id, categoria.Nombre, categoria.Descripcion, categoria.Activa);
    }

    public async Task<List<ProductoGastronomiaDto>> ObtenerProductosAsync(CancellationToken cancellationToken) =>
        (await _repositorio.ObtenerProductosAsync(cancellationToken))
        .Select(p => new ProductoGastronomiaDto(p.Id, p.Nombre, p.CategoriaGastronomiaId, p.Categoria?.Nombre ?? "Sin categoría", p.PrecioVenta, p.Activo)).ToList();

    public async Task<ProductoGastronomiaDto> CrearProductoAsync(CrearProductoGastronomiaDto dto, CancellationToken cancellationToken)
    {
        var categoria = await _repositorio.ObtenerCategoriaPorIdAsync(dto.CategoriaGastronomiaId, cancellationToken)
            ?? throw new ValidacionNegocioException("Categoría de gastronomía no encontrada.", 404);

        var producto = new ProductoGastronomia
        {
            Nombre = ValidadorEntrada.Requerido(dto.Nombre, nameof(dto.Nombre)),
            CategoriaGastronomiaId = categoria.Id,
            PrecioVenta = ValidadorEntrada.MayorACero(dto.PrecioVenta, nameof(dto.PrecioVenta))
        };

        await _repositorio.AgregarProductoAsync(producto, cancellationToken);
        await _repositorio.GuardarCambiosAsync(cancellationToken);
        return new ProductoGastronomiaDto(producto.Id, producto.Nombre, producto.CategoriaGastronomiaId, categoria.Nombre, producto.PrecioVenta, producto.Activo);
    }

    public async Task<List<InsumoGastronomiaDto>> ObtenerInsumosAsync(CancellationToken cancellationToken) =>
        (await _repositorio.ObtenerInsumosAsync(cancellationToken))
        .Select(i => new InsumoGastronomiaDto(i.Id, i.Nombre, i.Unidad, i.CostoUnitario, i.StockActual, i.StockMinimo, i.Activo)).ToList();

    public async Task<InsumoGastronomiaDto> CrearInsumoAsync(CrearInsumoGastronomiaDto dto, CancellationToken cancellationToken)
    {
        var insumo = new InsumoGastronomia
        {
            Nombre = ValidadorEntrada.Requerido(dto.Nombre, nameof(dto.Nombre)),
            Unidad = ValidadorEntrada.Requerido(dto.Unidad, nameof(dto.Unidad)),
            CostoUnitario = dto.CostoUnitario,
            StockActual = dto.StockActual,
            StockMinimo = dto.StockMinimo
        };

        await _repositorio.AgregarInsumoAsync(insumo, cancellationToken);
        await _repositorio.GuardarCambiosAsync(cancellationToken);

        return new InsumoGastronomiaDto(insumo.Id, insumo.Nombre, insumo.Unidad, insumo.CostoUnitario, insumo.StockActual, insumo.StockMinimo, insumo.Activo);
    }

    public async Task<RecetaProductoDto> GuardarRecetaAsync(GuardarRecetaProductoDto dto, CancellationToken cancellationToken)
    {
        if (dto.Items.Count == 0) throw new ValidacionNegocioException("La receta debe tener items.");
        var producto = await _repositorio.ObtenerProductoPorIdAsync(dto.ProductoGastronomiaId, cancellationToken)
            ?? throw new ValidacionNegocioException("Producto de gastronomía no encontrado.", 404);

        var receta = await _repositorio.ObtenerRecetaPorProductoAsync(producto.Id, cancellationToken);
        if (receta is null)
        {
            receta = new RecetaProducto { ProductoGastronomiaId = producto.Id };
            await _repositorio.AgregarRecetaAsync(receta, cancellationToken);
        }

        receta.Items.Clear();
        foreach (var item in dto.Items)
        {
            var insumo = await _repositorio.ObtenerInsumoPorIdAsync(item.InsumoGastronomiaId, cancellationToken)
                ?? throw new ValidacionNegocioException("Insumo de gastronomía no encontrado.", 404);

            receta.Items.Add(new RecetaItem
            {
                InsumoGastronomiaId = insumo.Id,
                Cantidad = ValidadorEntrada.MayorACero(item.Cantidad, nameof(item.Cantidad))
            });
        }

        await _repositorio.ActualizarRecetaAsync(receta, cancellationToken);
        await _repositorio.GuardarCambiosAsync(cancellationToken);
        return await ObtenerRecetaPorProductoAsync(producto.Id, cancellationToken);
    }

    public async Task<RecetaProductoDto> ObtenerRecetaPorProductoAsync(Guid productoId, CancellationToken cancellationToken)
    {
        var receta = await _repositorio.ObtenerRecetaPorProductoAsync(productoId, cancellationToken)
            ?? throw new ValidacionNegocioException("No existe receta para ese producto.", 404);

        var costo = receta.Items.Sum(i => i.Cantidad * (i.Insumo?.CostoUnitario ?? 0));

        return new RecetaProductoDto(
            receta.Id,
            receta.ProductoGastronomiaId,
            receta.Producto?.Nombre ?? "Producto",
            receta.Items.Select(i => new RecetaItemDto(i.InsumoGastronomiaId, i.Insumo?.Nombre ?? "Insumo", i.Cantidad, i.Insumo?.Unidad ?? "unidad")).ToList(),
            costo
        );
    }

    public async Task<List<PedidoGastronomiaDto>> ObtenerPedidosAsync(CancellationToken cancellationToken) =>
        (await _repositorio.ObtenerPedidosAsync(cancellationToken)).Select(MapearPedido).ToList();

    public async Task<PedidoGastronomiaDto> CrearPedidoAsync(CrearPedidoGastronomiaDto dto, CancellationToken cancellationToken)
    {
        var canal = ValidadorEntrada.Requerido(dto.Canal, nameof(dto.Canal)).ToLowerInvariant();
        if (!CanalesValidos.Contains(canal)) throw new ValidacionNegocioException("Canal inválido. Debe ser salon, mostrador o delivery.");
        if (dto.Items.Count == 0) throw new ValidacionNegocioException("El pedido debe tener items.");

        var pedido = new PedidoGastronomia
        {
            Canal = canal,
            Estado = "nuevo",
            Cliente = (dto.Cliente ?? "Consumidor final").Trim(),
            Observaciones = (dto.Observaciones ?? string.Empty).Trim()
        };

        foreach (var item in dto.Items)
        {
            if (item.Cantidad <= 0) throw new ValidacionNegocioException("Cantidad inválida en item de pedido.");
            var producto = await _repositorio.ObtenerProductoPorIdAsync(item.ProductoGastronomiaId, cancellationToken)
                ?? throw new ValidacionNegocioException("Producto de gastronomía no encontrado.", 404);

            var pedidoItem = new PedidoItemGastronomia
            {
                ProductoGastronomiaId = producto.Id,
                Cantidad = item.Cantidad,
                PrecioUnitario = producto.PrecioVenta,
                Subtotal = producto.PrecioVenta * item.Cantidad
            };
            pedido.Total += pedidoItem.Subtotal;
            pedido.Items.Add(pedidoItem);
        }

        await _repositorio.AgregarPedidoAsync(pedido, cancellationToken);
        await _repositorio.GuardarCambiosAsync(cancellationToken);
        return MapearPedido(pedido);
    }

    public async Task<PedidoGastronomiaDto?> CambiarEstadoPedidoAsync(Guid pedidoId, CambiarEstadoPedidoGastronomiaDto dto, CancellationToken cancellationToken)
    {
        var pedido = await _repositorio.ObtenerPedidoPorIdAsync(pedidoId, cancellationToken);
        if (pedido is null) return null;

        var estado = ValidadorEntrada.Requerido(dto.Estado, nameof(dto.Estado)).ToLowerInvariant();
        if (!EstadosPedidoValidos.Contains(estado)) throw new ValidacionNegocioException("Estado de pedido inválido.");

        pedido.Estado = estado;

        if (estado == "entregado")
        {
            await _repositorio.EjecutarEnTransaccionAsync(async ct =>
            {
                foreach (var item in pedido.Items)
                {
                    var receta = await _repositorio.ObtenerRecetaPorProductoAsync(item.ProductoGastronomiaId, ct);
                    if (receta is null) continue;

                    foreach (var recetaItem in receta.Items)
                    {
                        var insumo = await _repositorio.ObtenerInsumoPorIdAsync(recetaItem.InsumoGastronomiaId, ct);
                        if (insumo is null) continue;

                        var cantidadSalida = recetaItem.Cantidad * item.Cantidad;
                        insumo.StockActual -= cantidadSalida;

                        await _repositorio.AgregarMovimientoStockAsync(new MovimientoStockGastronomia
                        {
                            InsumoGastronomiaId = insumo.Id,
                            TipoMovimiento = "salida",
                            Cantidad = cantidadSalida,
                            Referencia = $"pedido:{pedido.Id}"
                        }, ct);
                    }
                }

                await _servicioImpacto.RegistrarImpactoOperacionAsync(
                    moduloOrigen: "gastronomia",
                    tipoOperacion: "pedido_entregado",
                    referenciaExterna: $"pedido:{pedido.Id}",
                    monto: pedido.Total,
                    descripcion: $"Pedido gastronomía entregado por canal {pedido.Canal}",
                    cancellationToken: ct
                );

                await _repositorio.GuardarCambiosAsync(ct);
            }, cancellationToken);
        }
        else
        {
            await _repositorio.GuardarCambiosAsync(cancellationToken);
        }

        return MapearPedido(pedido);
    }

    public async Task<List<PedidoGastronomiaDto>> ObtenerColaCocinaAsync(CancellationToken cancellationToken) =>
        (await _repositorio.ObtenerPedidosAsync(cancellationToken))
        .Where(p => p.Estado is "nuevo" or "en_preparacion" or "listo")
        .Select(MapearPedido)
        .ToList();

    public async Task<List<MovimientoStockGastronomiaDto>> ObtenerMovimientosStockAsync(CancellationToken cancellationToken)
    {
        var movimientos = await _repositorio.ObtenerMovimientosStockAsync(cancellationToken);
        return movimientos.Select(m => new MovimientoStockGastronomiaDto(
            m.Id,
            m.InsumoGastronomiaId,
            m.Insumo?.Nombre ?? "Insumo",
            m.TipoMovimiento,
            m.Cantidad,
            m.Referencia,
            m.FechaMovimientoUtc
        )).ToList();
    }

    private static PedidoGastronomiaDto MapearPedido(PedidoGastronomia pedido) =>
        new(
            pedido.Id,
            pedido.Canal,
            pedido.Estado,
            pedido.Cliente,
            pedido.Observaciones,
            pedido.FechaPedidoUtc,
            pedido.Total,
            pedido.Items.Select(i => new PedidoItemGastronomiaDto(i.ProductoGastronomiaId, i.Producto?.Nombre ?? "Producto", i.Cantidad, i.PrecioUnitario, i.Subtotal)).ToList()
        );
}
