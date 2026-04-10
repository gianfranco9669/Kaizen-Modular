using Kaizen.Dominio.Gastronomia;

namespace Kaizen.Aplicacion.Gastronomia.Interfaces;

public interface IGastronomiaRepositorio
{
    Task<List<CategoriaGastronomia>> ObtenerCategoriasAsync(CancellationToken cancellationToken);
    Task<CategoriaGastronomia?> ObtenerCategoriaPorIdAsync(Guid id, CancellationToken cancellationToken);
    Task AgregarCategoriaAsync(CategoriaGastronomia categoria, CancellationToken cancellationToken);

    Task<List<ProductoGastronomia>> ObtenerProductosAsync(CancellationToken cancellationToken);
    Task<ProductoGastronomia?> ObtenerProductoPorIdAsync(Guid id, CancellationToken cancellationToken);
    Task AgregarProductoAsync(ProductoGastronomia producto, CancellationToken cancellationToken);

    Task<List<InsumoGastronomia>> ObtenerInsumosAsync(CancellationToken cancellationToken);
    Task<InsumoGastronomia?> ObtenerInsumoPorIdAsync(Guid id, CancellationToken cancellationToken);
    Task AgregarInsumoAsync(InsumoGastronomia insumo, CancellationToken cancellationToken);

    Task<RecetaProducto?> ObtenerRecetaPorProductoAsync(Guid productoId, CancellationToken cancellationToken);
    Task AgregarRecetaAsync(RecetaProducto receta, CancellationToken cancellationToken);
    Task ActualizarRecetaAsync(RecetaProducto receta, CancellationToken cancellationToken);

    Task<List<PedidoGastronomia>> ObtenerPedidosAsync(CancellationToken cancellationToken);
    Task<PedidoGastronomia?> ObtenerPedidoPorIdAsync(Guid id, CancellationToken cancellationToken);
    Task AgregarPedidoAsync(PedidoGastronomia pedido, CancellationToken cancellationToken);

    Task AgregarMovimientoStockAsync(MovimientoStockGastronomia movimiento, CancellationToken cancellationToken);
    Task<List<MovimientoStockGastronomia>> ObtenerMovimientosStockAsync(CancellationToken cancellationToken);

    Task GuardarCambiosAsync(CancellationToken cancellationToken);
    Task EjecutarEnTransaccionAsync(Func<CancellationToken, Task> operacion, CancellationToken cancellationToken);
}
