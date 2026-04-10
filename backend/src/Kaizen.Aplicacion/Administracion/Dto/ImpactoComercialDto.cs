namespace Kaizen.Aplicacion.Administracion.Dto;

public record ImpactoComercialDto(
    Guid Id,
    string ModuloOrigen,
    string TipoOperacion,
    string ReferenciaExterna,
    string Descripcion,
    decimal Monto,
    DateTime FechaOperacionUtc
);
