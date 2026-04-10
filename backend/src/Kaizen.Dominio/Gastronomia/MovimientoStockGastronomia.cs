using Kaizen.Dominio.Compartido;

namespace Kaizen.Dominio.Gastronomia;

public class MovimientoStockGastronomia : EntidadBase
{
    public Guid InsumoGastronomiaId { get; set; }
    public InsumoGastronomia? Insumo { get; set; }
    public string TipoMovimiento { get; set; } = "salida";
    public decimal Cantidad { get; set; }
    public string Referencia { get; set; } = string.Empty;
    public DateTime FechaMovimientoUtc { get; set; } = DateTime.UtcNow;
}
