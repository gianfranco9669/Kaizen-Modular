namespace Kaizen.Aplicacion.Gimnasio.Dto;

public record PlanDto(
    Guid Id,
    string Nombre,
    string Descripcion,
    decimal Precio,
    int DuracionDias,
    bool PermiteAcceso,
    bool Activo
);

public record CrearPlanDto(
    string Nombre,
    string Descripcion,
    decimal Precio,
    int DuracionDias,
    bool PermiteAcceso
);
