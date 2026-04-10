from django.contrib import admin

from .models import RegistroImpactoComercial


@admin.register(RegistroImpactoComercial)
class RegistroImpactoComercialAdmin(admin.ModelAdmin):
    list_display = ('fecha_registro', 'origen', 'operacion', 'referencia_externa', 'monto')
    list_filter = ('origen', 'operacion')
    search_fields = ('referencia_externa', 'descripcion')
