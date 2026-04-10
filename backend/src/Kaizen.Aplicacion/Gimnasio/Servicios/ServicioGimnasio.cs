using Kaizen.Aplicacion.Administracion.Servicios;
using Kaizen.Aplicacion.Gimnasio.Dto;
using Kaizen.Aplicacion.Gimnasio.Interfaces;
using Kaizen.Dominio.Gimnasio;

namespace Kaizen.Aplicacion.Gimnasio.Servicios;

public class ServicioGimnasio
{
    private readonly IGimnasioRepositorio _repositorio;
    private readonly ServicioImpactoAdministrativo _servicioImpactoAdministrativo;

    public ServicioGimnasio(IGimnasioRepositorio repositorio, ServicioImpactoAdministrativo servicioImpactoAdministrativo)
    {
        _repositorio = repositorio;
        _servicioImpactoAdministrativo = servicioImpactoAdministrativo;
    }

    public async Task<List<SocioDto>> ObtenerSociosAsync(CancellationToken cancellationToken)
    {
        var socios = await _repositorio.ObtenerSociosAsync(cancellationToken);
        return socios.Select(MapearSocio).ToList();
    }

    public async Task<SocioDto> CrearSocioAsync(CrearSocioDto dto, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(dto.NumeroSocio)) throw new ArgumentException("Número de socio requerido.");
        if (string.IsNullOrWhiteSpace(dto.Documento)) throw new ArgumentException("Documento requerido.");

        var socio = new Socio
        {
            NumeroSocio = dto.NumeroSocio.Trim(),
            Nombre = dto.Nombre.Trim(),
            Apellido = dto.Apellido.Trim(),
            Documento = dto.Documento.Trim(),
            Correo = dto.Correo.Trim(),
            Telefono = dto.Telefono.Trim(),
            Activo = true
        };

        await _repositorio.AgregarSocioAsync(socio, cancellationToken);
        await _repositorio.GuardarCambiosAsync(cancellationToken);
        return MapearSocio(socio);
    }

    public async Task<SocioDto?> ActualizarSocioAsync(Guid socioId, CrearSocioDto dto, CancellationToken cancellationToken)
    {
        var socio = await _repositorio.ObtenerSocioPorIdAsync(socioId, cancellationToken);
        if (socio is null) return null;

        socio.NumeroSocio = dto.NumeroSocio.Trim();
        socio.Nombre = dto.Nombre.Trim();
        socio.Apellido = dto.Apellido.Trim();
        socio.Documento = dto.Documento.Trim();
        socio.Correo = dto.Correo.Trim();
        socio.Telefono = dto.Telefono.Trim();
        socio.FechaActualizacionUtc = DateTime.UtcNow;

        await _repositorio.ActualizarSocioAsync(socio, cancellationToken);
        await _repositorio.GuardarCambiosAsync(cancellationToken);
        return MapearSocio(socio);
    }

    public async Task<List<PlanDto>> ObtenerPlanesAsync(CancellationToken cancellationToken)
    {
        var planes = await _repositorio.ObtenerPlanesAsync(cancellationToken);
        return planes.Select(MapearPlan).ToList();
    }

    public async Task<PlanDto> CrearPlanAsync(CrearPlanDto dto, CancellationToken cancellationToken)
    {
        if (dto.Precio <= 0) throw new ArgumentException("El precio debe ser mayor a cero.");
        if (dto.DuracionDias <= 0) throw new ArgumentException("La duración debe ser mayor a cero.");

        var plan = new Plan
        {
            Nombre = dto.Nombre.Trim(),
            Descripcion = dto.Descripcion.Trim(),
            Precio = dto.Precio,
            DuracionDias = dto.DuracionDias,
            PermiteAcceso = dto.PermiteAcceso,
            Activo = true
        };

        await _repositorio.AgregarPlanAsync(plan, cancellationToken);
        await _repositorio.GuardarCambiosAsync(cancellationToken);
        return MapearPlan(plan);
    }

    public async Task<PlanDto?> ActualizarPlanAsync(Guid planId, CrearPlanDto dto, CancellationToken cancellationToken)
    {
        var plan = await _repositorio.ObtenerPlanPorIdAsync(planId, cancellationToken);
        if (plan is null) return null;

        plan.Nombre = dto.Nombre.Trim();
        plan.Descripcion = dto.Descripcion.Trim();
        plan.Precio = dto.Precio;
        plan.DuracionDias = dto.DuracionDias;
        plan.PermiteAcceso = dto.PermiteAcceso;
        plan.FechaActualizacionUtc = DateTime.UtcNow;

        await _repositorio.ActualizarPlanAsync(plan, cancellationToken);
        await _repositorio.GuardarCambiosAsync(cancellationToken);
        return MapearPlan(plan);
    }

    public async Task<List<MembresiaDto>> ObtenerMembresiasAsync(CancellationToken cancellationToken)
    {
        var membresias = await _repositorio.ObtenerMembresiasAsync(cancellationToken);
        return membresias.Select(MapearMembresia).ToList();
    }

    public async Task<MembresiaDto> CrearMembresiaAsync(CrearMembresiaDto dto, CancellationToken cancellationToken)
    {
        var socio = await _repositorio.ObtenerSocioPorIdAsync(dto.SocioId, cancellationToken)
            ?? throw new ArgumentException("Socio no encontrado.");

        var plan = await _repositorio.ObtenerPlanPorIdAsync(dto.PlanId, cancellationToken)
            ?? throw new ArgumentException("Plan no encontrado.");

        var fechaFin = dto.FechaInicio.AddDays(plan.DuracionDias);
        var estadoDeuda = dto.MontoAdeudado > 0 ? "pendiente" : "al_dia";

        var membresia = new Membresia
        {
            SocioId = socio.Id,
            PlanId = plan.Id,
            FechaInicio = dto.FechaInicio,
            FechaFin = fechaFin,
            EstadoVigencia = "vigente",
            EstadoDeuda = estadoDeuda,
            MontoTotal = plan.Precio,
            MontoAdeudado = dto.MontoAdeudado
        };

        await _repositorio.AgregarMembresiaAsync(membresia, cancellationToken);
        await _repositorio.GuardarCambiosAsync(cancellationToken);

        await _servicioImpactoAdministrativo.RegistrarImpactoMembresiaAsync(
            membresia.Id,
            membresia.SocioId,
            membresia.MontoTotal,
            cancellationToken
        );

        return MapearMembresia(membresia);
    }

    public async Task<ResultadoAccesoDto> ValidarYRegistrarAccesoAsync(Guid socioId, CancellationToken cancellationToken)
    {
        var socio = await _repositorio.ObtenerSocioPorIdAsync(socioId, cancellationToken)
            ?? throw new ArgumentException("Socio no encontrado.");

        var membresia = socio.Membresias
            .OrderByDescending(m => m.FechaFin)
            .FirstOrDefault();

        string resultado;
        string motivo;

        if (membresia is null)
        {
            resultado = "bloqueado";
            motivo = "Sin membresía activa.";
        }
        else if (membresia.FechaFin < DateOnly.FromDateTime(DateTime.UtcNow))
        {
            resultado = "bloqueado";
            motivo = "Membresía vencida.";
        }
        else if (membresia.EstadoDeuda is "pendiente" or "moroso" || membresia.MontoAdeudado > 0)
        {
            resultado = "bloqueado";
            motivo = "Membresía con deuda.";
        }
        else
        {
            resultado = "permitido";
            motivo = "Ingreso autorizado.";
        }

        var registro = new RegistroAcceso
        {
            SocioId = socioId,
            Resultado = resultado,
            Motivo = motivo,
            FechaHoraUtc = DateTime.UtcNow
        };

        await _repositorio.AgregarRegistroAccesoAsync(registro, cancellationToken);
        await _repositorio.GuardarCambiosAsync(cancellationToken);

        return new ResultadoAccesoDto(registro.Id, resultado, motivo, registro.FechaHoraUtc);
    }

    private static SocioDto MapearSocio(Socio s) => new(s.Id, s.NumeroSocio, s.Nombre, s.Apellido, s.Documento, s.Correo, s.Telefono, s.Activo);
    private static PlanDto MapearPlan(Plan p) => new(p.Id, p.Nombre, p.Descripcion, p.Precio, p.DuracionDias, p.PermiteAcceso, p.Activo);
    private static MembresiaDto MapearMembresia(Membresia m) => new(m.Id, m.SocioId, m.PlanId, m.FechaInicio, m.FechaFin, m.EstadoVigencia, m.EstadoDeuda, m.MontoTotal, m.MontoAdeudado);
}
