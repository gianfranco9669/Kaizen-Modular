namespace Kaizen.Aplicacion.Gimnasio.Dto;

public record MembresiaDto(
    Guid Id,
    Guid SocioId,
    Guid PlanId,
    DateOnly FechaInicio,
    DateOnly FechaFin,
    string EstadoVigencia,
    string EstadoDeuda,
    decimal MontoTotal,
    decimal MontoAdeudado
);

public record CrearMembresiaDto(
    Guid SocioId,
    Guid PlanId,
    DateOnly FechaInicio,
    decimal MontoAdeudado
);

public record ValidarAccesoDto(Guid SocioId);

public record ResultadoAccesoDto(
    Guid RegistroAccesoId,
    string Resultado,
    string Motivo,
    DateTime FechaHoraUtc
);
