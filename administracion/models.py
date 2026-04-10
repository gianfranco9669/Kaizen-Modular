from django.db import models


class RegistroImpactoComercial(models.Model):
    ORIGEN_GIMNASIO = 'gimnasio'
    ORIGEN_GASTRONOMIA = 'gastronomia'
    ORIGEN_CHOICES = [
        (ORIGEN_GIMNASIO, 'Gimnasio'),
        (ORIGEN_GASTRONOMIA, 'Gastronomía'),
    ]

    OPERACION_VENTA_PLAN = 'venta_plan'
    OPERACION_RENOVACION = 'renovacion_membresia'
    OPERACION_CHOICES = [
        (OPERACION_VENTA_PLAN, 'Venta de plan'),
        (OPERACION_RENOVACION, 'Renovación de membresía'),
    ]

    origen = models.CharField(max_length=30, choices=ORIGEN_CHOICES)
    operacion = models.CharField(max_length=50, choices=OPERACION_CHOICES)
    referencia_externa = models.CharField(max_length=120)
    descripcion = models.CharField(max_length=255)
    monto = models.DecimalField(max_digits=12, decimal_places=2)
    fecha_registro = models.DateTimeField(auto_now_add=True)

    class Meta:
        verbose_name = 'Registro de impacto comercial'
        verbose_name_plural = 'Registros de impacto comercial'
        ordering = ('-fecha_registro',)

    def __str__(self) -> str:
        return f'{self.get_origen_display()} - {self.get_operacion_display()} - {self.monto}'
