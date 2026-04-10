using Kaizen.Dominio.Compartido;

namespace Kaizen.Dominio.Administracion;

public class ImpactoComercial : EntidadBase
{
    public string ModuloOrigen { get; set; } = string.Empty;
    public string TipoOperacion { get; set; } = string.Empty;
    public string ReferenciaExterna { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public decimal Monto { get; set; }
    public DateTime FechaOperacionUtc { get; set; } = DateTime.UtcNow;
}
