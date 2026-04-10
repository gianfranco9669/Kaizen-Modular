from datetime import timedelta

from django import forms

from nucleo.models import Persona

from .models import Membresia, Plan, Socio


class PersonaForm(forms.ModelForm):
    class Meta:
        model = Persona
        fields = ['nombre', 'apellido', 'tipo_documento', 'numero_documento', 'correo', 'telefono']


class SocioForm(forms.ModelForm):
    class Meta:
        model = Socio
        fields = ['numero_socio', 'sede', 'estado', 'fecha_alta']


class PlanForm(forms.ModelForm):
    class Meta:
        model = Plan
        fields = ['nombre', 'descripcion', 'precio', 'duracion_dias', 'permite_acceso', 'activo']


class MembresiaForm(forms.ModelForm):
    class Meta:
        model = Membresia
        fields = ['socio', 'plan', 'fecha_inicio', 'fecha_fin', 'estado', 'estado_deuda', 'monto_total', 'monto_adeudado']

    def clean(self):
        cleaned_data = super().clean()
        fecha_inicio = cleaned_data.get('fecha_inicio')
        plan = cleaned_data.get('plan')
        fecha_fin = cleaned_data.get('fecha_fin')
        monto_total = cleaned_data.get('monto_total')

        if plan and fecha_inicio and not fecha_fin:
            cleaned_data['fecha_fin'] = fecha_inicio + timedelta(days=plan.duracion_dias)

        if plan and monto_total is None:
            cleaned_data['monto_total'] = plan.precio

        return cleaned_data
