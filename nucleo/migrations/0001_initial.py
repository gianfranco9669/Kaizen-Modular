from django.db import migrations, models


class Migration(migrations.Migration):

    initial = True

    dependencies = []

    operations = [
        migrations.CreateModel(
            name='Sede',
            fields=[
                ('id', models.BigAutoField(auto_created=True, primary_key=True, serialize=False, verbose_name='ID')),
                ('nombre', models.CharField(max_length=120, unique=True)),
                ('codigo', models.CharField(max_length=20, unique=True)),
                ('activa', models.BooleanField(default=True)),
            ],
            options={
                'verbose_name': 'Sede',
                'verbose_name_plural': 'Sedes',
                'permissions': [('puede_ver_todas_las_sedes', 'Puede visualizar datos de todas las sedes')],
            },
        ),
        migrations.CreateModel(
            name='Persona',
            fields=[
                ('id', models.BigAutoField(auto_created=True, primary_key=True, serialize=False, verbose_name='ID')),
                ('nombre', models.CharField(max_length=120)),
                ('apellido', models.CharField(max_length=120)),
                ('tipo_documento', models.CharField(choices=[('dni', 'DNI'), ('cuit', 'CUIT')], default='dni', max_length=10)),
                ('numero_documento', models.CharField(max_length=20)),
                ('correo', models.EmailField(blank=True, max_length=254)),
                ('telefono', models.CharField(blank=True, max_length=40)),
                ('fecha_alta', models.DateTimeField(auto_now_add=True)),
            ],
            options={'verbose_name': 'Persona', 'verbose_name_plural': 'Personas'},
        ),
        migrations.AddConstraint(
            model_name='persona',
            constraint=models.UniqueConstraint(fields=('tipo_documento', 'numero_documento'), name='uq_persona_documento'),
        ),
    ]
