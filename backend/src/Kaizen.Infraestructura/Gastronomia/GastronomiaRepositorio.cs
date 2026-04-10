using Kaizen.Aplicacion.Gastronomia.Interfaces;
using Kaizen.Dominio.Gastronomia;
using Kaizen.Infraestructura.Persistencia;
using Microsoft.EntityFrameworkCore;

namespace Kaizen.Infraestructura.Gastronomia;

public class GastronomiaRepositorio : IGastronomiaRepositorio
{
    private readonly KaizenDbContext _db;

    public GastronomiaRepositorio(KaizenDbContext db)
    {
        _db = db;
    }

    public Task<List<CategoriaGastronomia>> ObtenerCategoriasAsync(CancellationToken cancellationToken) =>
        _db.CategoriasGastronomia.AsNoTracking().OrderBy(x => x.Nombre).ToListAsync(cancellationToken);

    public Task<CategoriaGastronomia?> ObtenerCategoriaPorIdAsync(Guid id, CancellationToken cancellationToken) =>
        _db.CategoriasGastronomia.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public Task AgregarCategoriaAsync(CategoriaGastronomia categoria, CancellationToken cancellationToken) =>
        _db.CategoriasGastronomia.AddAsync(categoria, cancellationToken).AsTask();

    public Task<List<ProductoGastronomia>> ObtenerProductosAsync(CancellationToken cancellationToken) =>
        _db.ProductosGastronomia.AsNoTracking().Include(x => x.Categoria).OrderBy(x => x.Nombre).ToListAsync(cancellationToken);

    public Task<ProductoGastronomia?> ObtenerProductoPorIdAsync(Guid id, CancellationToken cancellationToken) =>
        _db.ProductosGastronomia.Include(x => x.Categoria).FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public Task AgregarProductoAsync(ProductoGastronomia producto, CancellationToken cancellationToken) =>
        _db.ProductosGastronomia.AddAsync(producto, cancellationToken).AsTask();

    public Task<List<InsumoGastronomia>> ObtenerInsumosAsync(CancellationToken cancellationToken) =>
        _db.InsumosGastronomia.AsNoTracking().OrderBy(x => x.Nombre).ToListAsync(cancellationToken);

    public Task<InsumoGastronomia?> ObtenerInsumoPorIdAsync(Guid id, CancellationToken cancellationToken) =>
        _db.InsumosGastronomia.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public Task AgregarInsumoAsync(InsumoGastronomia insumo, CancellationToken cancellationToken) =>
        _db.InsumosGastronomia.AddAsync(insumo, cancellationToken).AsTask();

    public Task<RecetaProducto?> ObtenerRecetaPorProductoAsync(Guid productoId, CancellationToken cancellationToken) =>
        _db.RecetasProducto
            .Include(r => r.Producto)
            .Include(r => r.Items)
            .ThenInclude(i => i.Insumo)
            .FirstOrDefaultAsync(r => r.ProductoGastronomiaId == productoId, cancellationToken);

    public Task AgregarRecetaAsync(RecetaProducto receta, CancellationToken cancellationToken) =>
        _db.RecetasProducto.AddAsync(receta, cancellationToken).AsTask();

    public Task ActualizarRecetaAsync(RecetaProducto receta, CancellationToken cancellationToken)
    {
        _db.RecetasProducto.Update(receta);
        return Task.CompletedTask;
    }

    public Task<List<PedidoGastronomia>> ObtenerPedidosAsync(CancellationToken cancellationToken) =>
        _db.PedidosGastronomia.AsNoTracking().Include(p => p.Items).ThenInclude(i => i.Producto).OrderByDescending(p => p.FechaPedidoUtc).ToListAsync(cancellationToken);

    public Task<PedidoGastronomia?> ObtenerPedidoPorIdAsync(Guid id, CancellationToken cancellationToken) =>
        _db.PedidosGastronomia.Include(p => p.Items).ThenInclude(i => i.Producto).FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

    public Task AgregarPedidoAsync(PedidoGastronomia pedido, CancellationToken cancellationToken) =>
        _db.PedidosGastronomia.AddAsync(pedido, cancellationToken).AsTask();

    public Task AgregarMovimientoStockAsync(MovimientoStockGastronomia movimiento, CancellationToken cancellationToken) =>
        _db.MovimientosStockGastronomia.AddAsync(movimiento, cancellationToken).AsTask();

    public Task<List<MovimientoStockGastronomia>> ObtenerMovimientosStockAsync(CancellationToken cancellationToken) =>
        _db.MovimientosStockGastronomia.AsNoTracking().Include(m => m.Insumo).OrderByDescending(m => m.FechaMovimientoUtc).ToListAsync(cancellationToken);

    public Task GuardarCambiosAsync(CancellationToken cancellationToken) => _db.SaveChangesAsync(cancellationToken);

    public async Task EjecutarEnTransaccionAsync(Func<CancellationToken, Task> operacion, CancellationToken cancellationToken)
    {
        await using var tx = await _db.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            await operacion(cancellationToken);
            await tx.CommitAsync(cancellationToken);
        }
        catch
        {
            await tx.RollbackAsync(cancellationToken);
            throw;
        }
    }
}
