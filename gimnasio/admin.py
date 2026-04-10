from django.contrib import admin

from .models import Membresia, Plan, RegistroAcceso, Socio


@admin.register(Socio)
class SocioAdmin(admin.ModelAdmin):
    list_display = ('numero_socio', 'persona', 'sede', 'estado', 'fecha_alta')
    list_filter = ('estado', 'sede')
    search_fields = ('numero_socio', 'persona__nombre', 'persona__apellido')


@admin.register(Plan)
class PlanAdmin(admin.ModelAdmin):
    list_display = ('nombre', 'precio', 'duracion_dias', 'permite_acceso', 'activo')
    list_filter = ('activo', 'permite_acceso')
    search_fields = ('nombre',)


@admin.register(Membresia)
class MembresiaAdmin(admin.ModelAdmin):
    list_display = ('socio', 'plan', 'fecha_inicio', 'fecha_fin', 'estado', 'estado_deuda', 'monto_total', 'monto_adeudado')
    list_filter = ('estado', 'estado_deuda')
    search_fields = ('socio__numero_socio',)


@admin.register(RegistroAcceso)
class RegistroAccesoAdmin(admin.ModelAdmin):
    list_display = ('socio', 'fecha_hora', 'resultado', 'motivo')
    list_filter = ('resultado',)
    search_fields = ('socio__numero_socio', 'motivo')
