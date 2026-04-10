from administracion.models import RegistroImpactoComercial

from .models import Membresia, RegistroAcceso, Socio


def registrar_impacto_comercial_membresia(membresia: Membresia, operacion: str) -> None:
    RegistroImpactoComercial.objects.create(
        origen=RegistroImpactoComercial.ORIGEN_GIMNASIO,
        operacion=operacion,
        referencia_externa=f'membresia:{membresia.id}',
        descripcion=f'Membresía {membresia.plan.nombre} para socio {membresia.socio.numero_socio}',
        monto=membresia.monto_total,
    )


def validar_acceso_socio(socio: Socio) -> RegistroAcceso:
    membresia = socio.membresias.order_by('-fecha_fin').first()
    if not membresia:
        return RegistroAcceso.objects.create(
            socio=socio,
            resultado=RegistroAcceso.RESULTADO_BLOQUEADO,
            motivo='No posee membresía activa.',
        )

    if not membresia.vigente:
        return RegistroAcceso.objects.create(
            socio=socio,
            resultado=RegistroAcceso.RESULTADO_BLOQUEADO,
            motivo='Membresía vencida o cancelada.',
        )

    if membresia.tiene_deuda:
        return RegistroAcceso.objects.create(
            socio=socio,
            resultado=RegistroAcceso.RESULTADO_BLOQUEADO,
            motivo='Membresía con deuda pendiente.',
        )

    if not membresia.plan.permite_acceso:
        return RegistroAcceso.objects.create(
            socio=socio,
            resultado=RegistroAcceso.RESULTADO_BLOQUEADO,
            motivo='El plan no permite acceso por regla comercial.',
        )

    return RegistroAcceso.objects.create(
        socio=socio,
        resultado=RegistroAcceso.RESULTADO_PERMITIDO,
        motivo='Acceso permitido: membresía vigente y sin deuda.',
    )
