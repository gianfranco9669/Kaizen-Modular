using Kaizen.Dominio.Compartido;

namespace Kaizen.Dominio.Gimnasio;

public class Socio : EntidadBase
{
    public string NumeroSocio { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public string Apellido { get; set; } = string.Empty;
    public string Documento { get; set; } = string.Empty;
    public string Correo { get; set; } = string.Empty;
    public string Telefono { get; set; } = string.Empty;
    public bool Activo { get; set; } = true;

    public ICollection<Membresia> Membresias { get; set; } = new List<Membresia>();
}
