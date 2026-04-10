using Kaizen.Dominio.Compartido;

namespace Kaizen.Dominio.Gastronomia;

public class CategoriaGastronomia : EntidadBase
{
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public bool Activa { get; set; } = true;
}
