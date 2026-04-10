using Kaizen.Dominio.Compartido;

namespace Kaizen.Dominio.Gimnasio;

public class Plan : EntidadBase
{
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public decimal Precio { get; set; }
    public int DuracionDias { get; set; } = 30;
    public bool PermiteAcceso { get; set; } = true;
    public bool Activo { get; set; } = true;
}
