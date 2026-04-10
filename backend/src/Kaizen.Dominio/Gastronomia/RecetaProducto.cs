using Kaizen.Dominio.Compartido;

namespace Kaizen.Dominio.Gastronomia;

public class RecetaProducto : EntidadBase
{
    public Guid ProductoGastronomiaId { get; set; }
    public ProductoGastronomia? Producto { get; set; }
    public bool Activa { get; set; } = true;
    public List<RecetaItem> Items { get; set; } = [];
}
