namespace Kaizen.Aplicacion.Gimnasio.Dto;

public record SocioDto(
    Guid Id,
    string NumeroSocio,
    string Nombre,
    string Apellido,
    string Documento,
    string Correo,
    string Telefono,
    bool Activo
);

public record CrearSocioDto(
    string NumeroSocio,
    string Nombre,
    string Apellido,
    string Documento,
    string Correo,
    string Telefono
);
