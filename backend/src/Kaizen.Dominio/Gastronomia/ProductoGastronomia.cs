using Kaizen.Dominio.Compartido;

namespace Kaizen.Dominio.Gastronomia;

public class ProductoGastronomia : EntidadBase
{
    public string Nombre { get; set; } = string.Empty;
    public Guid CategoriaGastronomiaId { get; set; }
    public CategoriaGastronomia? Categoria { get; set; }
    public decimal PrecioVenta { get; set; }
    public bool Activo { get; set; } = true;
}
