from django.db import migrations, models


class Migration(migrations.Migration):

    initial = True

    dependencies = []

    operations = [
        migrations.CreateModel(
            name='RegistroImpactoComercial',
            fields=[
                ('id', models.BigAutoField(auto_created=True, primary_key=True, serialize=False, verbose_name='ID')),
                ('origen', models.CharField(choices=[('gimnasio', 'Gimnasio'), ('gastronomia', 'Gastronomía')], max_length=30)),
                ('operacion', models.CharField(choices=[('venta_plan', 'Venta de plan'), ('renovacion_membresia', 'Renovación de membresía')], max_length=50)),
                ('referencia_externa', models.CharField(max_length=120)),
                ('descripcion', models.CharField(max_length=255)),
                ('monto', models.DecimalField(decimal_places=2, max_digits=12)),
                ('fecha_registro', models.DateTimeField(auto_now_add=True)),
            ],
            options={
                'verbose_name': 'Registro de impacto comercial',
                'verbose_name_plural': 'Registros de impacto comercial',
                'ordering': ('-fecha_registro',),
            },
        ),
    ]
