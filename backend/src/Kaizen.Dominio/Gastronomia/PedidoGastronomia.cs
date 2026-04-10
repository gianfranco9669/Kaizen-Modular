using Kaizen.Dominio.Compartido;

namespace Kaizen.Dominio.Gastronomia;

public class PedidoGastronomia : EntidadBase
{
    public string Canal { get; set; } = "salon";
    public string Estado { get; set; } = "nuevo";
    public string Cliente { get; set; } = string.Empty;
    public string Observaciones { get; set; } = string.Empty;
    public DateTime FechaPedidoUtc { get; set; } = DateTime.UtcNow;
    public decimal Total { get; set; }
    public List<PedidoItemGastronomia> Items { get; set; } = [];
}
