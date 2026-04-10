from datetime import date
from decimal import Decimal

from django.core.exceptions import ValidationError
from django.db import models

from nucleo.models import Persona, Sede


class Socio(models.Model):
    ESTADO_ACTIVO = 'activo'
    ESTADO_INACTIVO = 'inactivo'
    ESTADO_CHOICES = [
        (ESTADO_ACTIVO, 'Activo'),
        (ESTADO_INACTIVO, 'Inactivo'),
    ]

    persona = models.OneToOneField(Persona, on_delete=models.PROTECT, related_name='socio')
    sede = models.ForeignKey(Sede, on_delete=models.PROTECT, related_name='socios')
    numero_socio = models.CharField(max_length=30, unique=True)
    estado = models.CharField(max_length=15, choices=ESTADO_CHOICES, default=ESTADO_ACTIVO)
    fecha_alta = models.DateField(default=date.today)

    class Meta:
        verbose_name = 'Socio'
        verbose_name_plural = 'Socios'
        permissions = [
            ('puede_gestionar_socios', 'Puede gestionar socios del gimnasio'),
        ]

    def __str__(self) -> str:
        return f'{self.numero_socio} - {self.persona}'


class Plan(models.Model):
    nombre = models.CharField(max_length=120, unique=True)
    descripcion = models.TextField(blank=True)
    precio = models.DecimalField(max_digits=10, decimal_places=2)
    duracion_dias = models.PositiveIntegerField(default=30)
    permite_acceso = models.BooleanField(default=True)
    activo = models.BooleanField(default=True)

    class Meta:
        verbose_name = 'Plan'
        verbose_name_plural = 'Planes'
        permissions = [
            ('puede_gestionar_planes', 'Puede gestionar planes del gimnasio'),
        ]

    def __str__(self) -> str:
        return self.nombre


class Membresia(models.Model):
    ESTADO_VIGENTE = 'vigente'
    ESTADO_VENCIDA = 'vencida'
    ESTADO_CANCELADA = 'cancelada'
    ESTADO_CHOICES = [
        (ESTADO_VIGENTE, 'Vigente'),
        (ESTADO_VENCIDA, 'Vencida'),
        (ESTADO_CANCELADA, 'Cancelada'),
    ]

    DEUDA_AL_DIA = 'al_dia'
    DEUDA_PENDIENTE = 'pendiente'
    DEUDA_MOROSO = 'moroso'
    DEUDA_CHOICES = [
        (DEUDA_AL_DIA, 'Al día'),
        (DEUDA_PENDIENTE, 'Pendiente'),
        (DEUDA_MOROSO, 'Moroso'),
    ]

    socio = models.ForeignKey(Socio, on_delete=models.PROTECT, related_name='membresias')
    plan = models.ForeignKey(Plan, on_delete=models.PROTECT, related_name='membresias')
    fecha_inicio = models.DateField(default=date.today)
    fecha_fin = models.DateField()
    estado = models.CharField(max_length=20, choices=ESTADO_CHOICES, default=ESTADO_VIGENTE)
    estado_deuda = models.CharField(max_length=20, choices=DEUDA_CHOICES, default=DEUDA_AL_DIA)
    monto_total = models.DecimalField(max_digits=10, decimal_places=2)
    monto_adeudado = models.DecimalField(max_digits=10, decimal_places=2, default=Decimal('0.00'))

    class Meta:
        verbose_name = 'Membresía'
        verbose_name_plural = 'Membresías'
        permissions = [
            ('puede_gestionar_membresias', 'Puede gestionar membresías'),
            ('puede_habilitar_acceso_con_deuda', 'Puede habilitar acceso con deuda'),
        ]

    def clean(self):
        if self.fecha_fin <= self.fecha_inicio:
            raise ValidationError('La fecha de fin debe ser posterior a la fecha de inicio.')
        if self.monto_adeudado > self.monto_total:
            raise ValidationError('El monto adeudado no puede superar el monto total.')

    @property
    def vigente(self) -> bool:
        return self.estado == self.ESTADO_VIGENTE and self.fecha_fin >= date.today()

    @property
    def tiene_deuda(self) -> bool:
        return self.estado_deuda in {self.DEUDA_PENDIENTE, self.DEUDA_MOROSO} or self.monto_adeudado > 0

    def __str__(self) -> str:
        return f'{self.socio.numero_socio} - {self.plan.nombre} ({self.fecha_inicio} a {self.fecha_fin})'


class RegistroAcceso(models.Model):
    RESULTADO_PERMITIDO = 'permitido'
    RESULTADO_BLOQUEADO = 'bloqueado'
    RESULTADO_CHOICES = [
        (RESULTADO_PERMITIDO, 'Permitido'),
        (RESULTADO_BLOQUEADO, 'Bloqueado'),
    ]

    socio = models.ForeignKey(Socio, on_delete=models.PROTECT, related_name='registros_acceso')
    fecha_hora = models.DateTimeField(auto_now_add=True)
    resultado = models.CharField(max_length=20, choices=RESULTADO_CHOICES)
    motivo = models.CharField(max_length=255)

    class Meta:
        verbose_name = 'Registro de acceso'
        verbose_name_plural = 'Registros de acceso'
        ordering = ('-fecha_hora',)

    def __str__(self) -> str:
        return f'{self.socio.numero_socio} - {self.resultado} - {self.fecha_hora:%d/%m/%Y %H:%M}'
