using Kaizen.Aplicacion.Administracion.Servicios;
using Kaizen.Aplicacion.Compartido;
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
        var socio = new Socio
        {
            NumeroSocio = ValidadorEntrada.Requerido(dto.NumeroSocio, nameof(dto.NumeroSocio)),
            Nombre = ValidadorEntrada.Requerido(dto.Nombre, nameof(dto.Nombre)),
            Apellido = ValidadorEntrada.Requerido(dto.Apellido, nameof(dto.Apellido)),
            Documento = ValidadorEntrada.Requerido(dto.Documento, nameof(dto.Documento)),
            Correo = (dto.Correo ?? string.Empty).Trim(),
            Telefono = (dto.Telefono ?? string.Empty).Trim(),
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

        socio.NumeroSocio = ValidadorEntrada.Requerido(dto.NumeroSocio, nameof(dto.NumeroSocio));
        socio.Nombre = ValidadorEntrada.Requerido(dto.Nombre, nameof(dto.Nombre));
        socio.Apellido = ValidadorEntrada.Requerido(dto.Apellido, nameof(dto.Apellido));
        socio.Documento = ValidadorEntrada.Requerido(dto.Documento, nameof(dto.Documento));
        socio.Correo = (dto.Correo ?? string.Empty).Trim();
        socio.Telefono = (dto.Telefono ?? string.Empty).Trim();
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
        var plan = new Plan
        {
            Nombre = ValidadorEntrada.Requerido(dto.Nombre, nameof(dto.Nombre)),
            Descripcion = (dto.Descripcion ?? string.Empty).Trim(),
            Precio = ValidadorEntrada.MayorACero(dto.Precio, nameof(dto.Precio)),
            DuracionDias = ValidadorEntrada.MayorACero(dto.DuracionDias, nameof(dto.DuracionDias)),
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

        plan.Nombre = ValidadorEntrada.Requerido(dto.Nombre, nameof(dto.Nombre));
        plan.Descripcion = (dto.Descripcion ?? string.Empty).Trim();
        plan.Precio = ValidadorEntrada.MayorACero(dto.Precio, nameof(dto.Precio));
        plan.DuracionDias = ValidadorEntrada.MayorACero(dto.DuracionDias, nameof(dto.DuracionDias));
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
        if (dto.MontoAdeudado < 0) throw new ValidacionNegocioException("El monto adeudado no puede ser negativo.");

        var socio = await _repositorio.ObtenerSocioPorIdAsync(dto.SocioId, cancellationToken)
            ?? throw new ValidacionNegocioException("Socio no encontrado.", 404);

        var plan = await _repositorio.ObtenerPlanPorIdAsync(dto.PlanId, cancellationToken)
            ?? throw new ValidacionNegocioException("Plan no encontrado.", 404);

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

        await _repositorio.EjecutarEnTransaccionAsync(async ct =>
        {
            await _repositorio.AgregarMembresiaAsync(membresia, ct);
            await _servicioImpactoAdministrativo.RegistrarImpactoMembresiaAsync(
                membresia.Id,
                membresia.SocioId,
                membresia.MontoTotal,
                ct
            );
            await _repositorio.GuardarCambiosAsync(ct);
        }, cancellationToken);

        return MapearMembresia(membresia);
    }

    public async Task<ResultadoAccesoDto> ValidarYRegistrarAccesoAsync(Guid socioId, CancellationToken cancellationToken)
    {
        var socio = await _repositorio.ObtenerSocioPorIdAsync(socioId, cancellationToken)
            ?? throw new ValidacionNegocioException("Socio no encontrado.", 404);

        var membresia = socio.Membresias.OrderByDescending(m => m.FechaFin).FirstOrDefault();

        var hoy = DateOnly.FromDateTime(DateTime.UtcNow);
        string resultado;
        string motivo;

        if (membresia is null)
        {
            resultado = "bloqueado";
            motivo = "Sin membresía activa.";
        }
        else if (membresia.FechaInicio > hoy)
        {
            resultado = "bloqueado";
            motivo = "Membresía aún no vigente.";
        }
        else if (membresia.FechaFin < hoy)
        {
            resultado = "bloqueado";
            motivo = "Membresía vencida.";
        }
        else if (membresia.Plan is not null && !membresia.Plan.PermiteAcceso)
        {
            resultado = "bloqueado";
            motivo = "El plan contratado no habilita ingreso.";
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
