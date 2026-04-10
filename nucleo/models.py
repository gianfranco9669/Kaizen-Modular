from django.db import models


class Sede(models.Model):
    nombre = models.CharField(max_length=120, unique=True)
    codigo = models.CharField(max_length=20, unique=True)
    activa = models.BooleanField(default=True)

    class Meta:
        verbose_name = 'Sede'
        verbose_name_plural = 'Sedes'
        permissions = [
            ('puede_ver_todas_las_sedes', 'Puede visualizar datos de todas las sedes'),
        ]

    def __str__(self) -> str:
        return f'{self.codigo} - {self.nombre}'


class Persona(models.Model):
    TIPO_DOCUMENTO_DNI = 'dni'
    TIPO_DOCUMENTO_CUIT = 'cuit'
    TIPO_DOCUMENTO_CHOICES = [
        (TIPO_DOCUMENTO_DNI, 'DNI'),
        (TIPO_DOCUMENTO_CUIT, 'CUIT'),
    ]

    nombre = models.CharField(max_length=120)
    apellido = models.CharField(max_length=120)
    tipo_documento = models.CharField(max_length=10, choices=TIPO_DOCUMENTO_CHOICES, default=TIPO_DOCUMENTO_DNI)
    numero_documento = models.CharField(max_length=20)
    correo = models.EmailField(blank=True)
    telefono = models.CharField(max_length=40, blank=True)
    fecha_alta = models.DateTimeField(auto_now_add=True)

    class Meta:
        verbose_name = 'Persona'
        verbose_name_plural = 'Personas'
        constraints = [
            models.UniqueConstraint(
                fields=['tipo_documento', 'numero_documento'],
                name='uq_persona_documento',
            )
        ]

    def __str__(self) -> str:
        return f'{self.apellido}, {self.nombre}'
