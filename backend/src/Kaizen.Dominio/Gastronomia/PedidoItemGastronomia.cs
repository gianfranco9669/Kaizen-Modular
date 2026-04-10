using Kaizen.Dominio.Compartido;

namespace Kaizen.Dominio.Gastronomia;

public class PedidoItemGastronomia : EntidadBase
{
    public Guid PedidoGastronomiaId { get; set; }
    public PedidoGastronomia? Pedido { get; set; }
    public Guid ProductoGastronomiaId { get; set; }
    public ProductoGastronomia? Producto { get; set; }
    public int Cantidad { get; set; }
    public decimal PrecioUnitario { get; set; }
    public decimal Subtotal { get; set; }
}
