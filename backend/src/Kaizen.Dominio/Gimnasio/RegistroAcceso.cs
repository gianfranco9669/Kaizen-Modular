using Kaizen.Dominio.Compartido;

namespace Kaizen.Dominio.Gimnasio;

public class RegistroAcceso : EntidadBase
{
    public Guid SocioId { get; set; }
    public Socio? Socio { get; set; }
    public DateTime FechaHoraUtc { get; set; } = DateTime.UtcNow;
    public string Resultado { get; set; } = string.Empty;
    public string Motivo { get; set; } = string.Empty;
}
