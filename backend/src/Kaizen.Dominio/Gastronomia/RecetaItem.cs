using Kaizen.Dominio.Compartido;

namespace Kaizen.Dominio.Gastronomia;

public class RecetaItem : EntidadBase
{
    public Guid RecetaProductoId { get; set; }
    public RecetaProducto? RecetaProducto { get; set; }
    public Guid InsumoGastronomiaId { get; set; }
    public InsumoGastronomia? Insumo { get; set; }
    public decimal Cantidad { get; set; }
}
