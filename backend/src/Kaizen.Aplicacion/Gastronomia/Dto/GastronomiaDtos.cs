namespace Kaizen.Aplicacion.Gastronomia.Dto;

public record CategoriaGastronomiaDto(Guid Id, string Nombre, string Descripcion, bool Activa);
public record CrearCategoriaGastronomiaDto(string Nombre, string? Descripcion);

public record ProductoGastronomiaDto(Guid Id, string Nombre, Guid CategoriaGastronomiaId, string CategoriaNombre, decimal PrecioVenta, bool Activo);
public record CrearProductoGastronomiaDto(string Nombre, Guid CategoriaGastronomiaId, decimal PrecioVenta);

public record InsumoGastronomiaDto(Guid Id, string Nombre, string Unidad, decimal CostoUnitario, decimal StockActual, decimal StockMinimo, bool Activo);
public record CrearInsumoGastronomiaDto(string Nombre, string Unidad, decimal CostoUnitario, decimal StockActual, decimal StockMinimo);

public record RecetaItemDto(Guid InsumoGastronomiaId, string InsumoNombre, decimal Cantidad, string Unidad);
public record RecetaProductoDto(Guid RecetaProductoId, Guid ProductoGastronomiaId, string ProductoNombre, List<RecetaItemDto> Items, decimal CostoEstimado);
public record RecetaItemInputDto(Guid InsumoGastronomiaId, decimal Cantidad);
public record GuardarRecetaProductoDto(Guid ProductoGastronomiaId, List<RecetaItemInputDto> Items);

public record PedidoItemGastronomiaDto(Guid ProductoGastronomiaId, string ProductoNombre, int Cantidad, decimal PrecioUnitario, decimal Subtotal);
public record PedidoGastronomiaDto(Guid Id, string Canal, string Estado, string Cliente, string Observaciones, DateTime FechaPedidoUtc, decimal Total, List<PedidoItemGastronomiaDto> Items);
public record CrearPedidoItemGastronomiaDto(Guid ProductoGastronomiaId, int Cantidad);
public record CrearPedidoGastronomiaDto(string Canal, string? Cliente, string? Observaciones, List<CrearPedidoItemGastronomiaDto> Items);
public record CambiarEstadoPedidoGastronomiaDto(string Estado);

public record MovimientoStockGastronomiaDto(Guid Id, Guid InsumoGastronomiaId, string InsumoNombre, string TipoMovimiento, decimal Cantidad, string Referencia, DateTime FechaMovimientoUtc);

public record DashboardGastronomiaDto(int Categorias, int Productos, int Insumos, int PedidosActivos, decimal VentasDia, int InsumosBajoMinimo);
