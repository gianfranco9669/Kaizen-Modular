import datetime
import django.db.models.deletion
from django.db import migrations, models


class Migration(migrations.Migration):

    initial = True

    dependencies = [
        ('nucleo', '0001_initial'),
    ]

    operations = [
        migrations.CreateModel(
            name='Plan',
            fields=[
                ('id', models.BigAutoField(auto_created=True, primary_key=True, serialize=False, verbose_name='ID')),
                ('nombre', models.CharField(max_length=120, unique=True)),
                ('descripcion', models.TextField(blank=True)),
                ('precio', models.DecimalField(decimal_places=2, max_digits=10)),
                ('duracion_dias', models.PositiveIntegerField(default=30)),
                ('permite_acceso', models.BooleanField(default=True)),
                ('activo', models.BooleanField(default=True)),
            ],
            options={
                'verbose_name': 'Plan',
                'verbose_name_plural': 'Planes',
                'permissions': [('puede_gestionar_planes', 'Puede gestionar planes del gimnasio')],
            },
        ),
        migrations.CreateModel(
            name='Socio',
            fields=[
                ('id', models.BigAutoField(auto_created=True, primary_key=True, serialize=False, verbose_name='ID')),
                ('numero_socio', models.CharField(max_length=30, unique=True)),
                ('estado', models.CharField(choices=[('activo', 'Activo'), ('inactivo', 'Inactivo')], default='activo', max_length=15)),
                ('fecha_alta', models.DateField(default=datetime.date.today)),
                ('persona', models.OneToOneField(on_delete=django.db.models.deletion.PROTECT, related_name='socio', to='nucleo.persona')),
                ('sede', models.ForeignKey(on_delete=django.db.models.deletion.PROTECT, related_name='socios', to='nucleo.sede')),
            ],
            options={
                'verbose_name': 'Socio',
                'verbose_name_plural': 'Socios',
                'permissions': [('puede_gestionar_socios', 'Puede gestionar socios del gimnasio')],
            },
        ),
        migrations.CreateModel(
            name='RegistroAcceso',
            fields=[
                ('id', models.BigAutoField(auto_created=True, primary_key=True, serialize=False, verbose_name='ID')),
                ('fecha_hora', models.DateTimeField(auto_now_add=True)),
                ('resultado', models.CharField(choices=[('permitido', 'Permitido'), ('bloqueado', 'Bloqueado')], max_length=20)),
                ('motivo', models.CharField(max_length=255)),
                ('socio', models.ForeignKey(on_delete=django.db.models.deletion.PROTECT, related_name='registros_acceso', to='gimnasio.socio')),
            ],
            options={
                'verbose_name': 'Registro de acceso',
                'verbose_name_plural': 'Registros de acceso',
                'ordering': ('-fecha_hora',),
            },
        ),
        migrations.CreateModel(
            name='Membresia',
            fields=[
                ('id', models.BigAutoField(auto_created=True, primary_key=True, serialize=False, verbose_name='ID')),
                ('fecha_inicio', models.DateField(default=datetime.date.today)),
                ('fecha_fin', models.DateField()),
                ('estado', models.CharField(choices=[('vigente', 'Vigente'), ('vencida', 'Vencida'), ('cancelada', 'Cancelada')], default='vigente', max_length=20)),
                ('estado_deuda', models.CharField(choices=[('al_dia', 'Al día'), ('pendiente', 'Pendiente'), ('moroso', 'Moroso')], default='al_dia', max_length=20)),
                ('monto_total', models.DecimalField(decimal_places=2, max_digits=10)),
                ('monto_adeudado', models.DecimalField(decimal_places=2, default='0.00', max_digits=10)),
                ('plan', models.ForeignKey(on_delete=django.db.models.deletion.PROTECT, related_name='membresias', to='gimnasio.plan')),
                ('socio', models.ForeignKey(on_delete=django.db.models.deletion.PROTECT, related_name='membresias', to='gimnasio.socio')),
            ],
            options={
                'verbose_name': 'Membresía',
                'verbose_name_plural': 'Membresías',
                'permissions': [
                    ('puede_gestionar_membresias', 'Puede gestionar membresías'),
                    ('puede_habilitar_acceso_con_deuda', 'Puede habilitar acceso con deuda'),
                ],
            },
        ),
    ]
