using Kaizen.Dominio.Gimnasio;

namespace Kaizen.Aplicacion.Gimnasio.Interfaces;

public interface IGimnasioRepositorio
{
    Task<List<Socio>> ObtenerSociosAsync(CancellationToken cancellationToken);
    Task<Socio?> ObtenerSocioPorIdAsync(Guid socioId, CancellationToken cancellationToken);
    Task AgregarSocioAsync(Socio socio, CancellationToken cancellationToken);
    Task ActualizarSocioAsync(Socio socio, CancellationToken cancellationToken);

    Task<List<Plan>> ObtenerPlanesAsync(CancellationToken cancellationToken);
    Task<Plan?> ObtenerPlanPorIdAsync(Guid planId, CancellationToken cancellationToken);
    Task AgregarPlanAsync(Plan plan, CancellationToken cancellationToken);
    Task ActualizarPlanAsync(Plan plan, CancellationToken cancellationToken);

    Task<List<Membresia>> ObtenerMembresiasAsync(CancellationToken cancellationToken);
    Task<Membresia?> ObtenerMembresiaPorIdAsync(Guid membresiaId, CancellationToken cancellationToken);
    Task AgregarMembresiaAsync(Membresia membresia, CancellationToken cancellationToken);
    Task ActualizarMembresiaAsync(Membresia membresia, CancellationToken cancellationToken);

    Task AgregarRegistroAccesoAsync(RegistroAcceso registroAcceso, CancellationToken cancellationToken);
    Task GuardarCambiosAsync(CancellationToken cancellationToken);
}
