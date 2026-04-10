using Kaizen.Dominio.Compartido;

namespace Kaizen.Dominio.Gimnasio;

public class Membresia : EntidadBase
{
    public Guid SocioId { get; set; }
    public Socio? Socio { get; set; }

    public Guid PlanId { get; set; }
    public Plan? Plan { get; set; }

    public DateOnly FechaInicio { get; set; }
    public DateOnly FechaFin { get; set; }
    public string EstadoVigencia { get; set; } = "vigente";
    public string EstadoDeuda { get; set; } = "al_dia";
    public decimal MontoTotal { get; set; }
    public decimal MontoAdeudado { get; set; }
}
