from django.contrib import admin

from .models import Persona, Sede


@admin.register(Sede)
class SedeAdmin(admin.ModelAdmin):
    list_display = ('codigo', 'nombre', 'activa')
    list_filter = ('activa',)
    search_fields = ('codigo', 'nombre')


@admin.register(Persona)
class PersonaAdmin(admin.ModelAdmin):
    list_display = ('apellido', 'nombre', 'tipo_documento', 'numero_documento', 'correo', 'telefono')
    list_filter = ('tipo_documento',)
    search_fields = ('apellido', 'nombre', 'numero_documento')
