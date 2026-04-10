using Kaizen.Dominio.Compartido;

namespace Kaizen.Dominio.Gastronomia;

public class InsumoGastronomia : EntidadBase
{
    public string Nombre { get; set; } = string.Empty;
    public string Unidad { get; set; } = "unidad";
    public decimal CostoUnitario { get; set; }
    public decimal StockActual { get; set; }
    public decimal StockMinimo { get; set; }
    public bool Activo { get; set; } = true;
}
