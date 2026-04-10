from datetime import timedelta

from django.contrib import messages
from django.shortcuts import get_object_or_404, redirect, render
from django.urls import reverse_lazy
from django.views.generic import CreateView, ListView

from nucleo.models import Persona

from .forms import MembresiaForm, PlanForm, PersonaForm, SocioForm
from .models import Membresia, Plan, Socio
from .servicios import registrar_impacto_comercial_membresia, validar_acceso_socio


class SocioListView(ListView):
    model = Socio
    template_name = 'gimnasio/socio_lista.html'
    context_object_name = 'socios'


def socio_crear(request):
    persona_form = PersonaForm(request.POST or None, prefix='persona')
    socio_form = SocioForm(request.POST or None, prefix='socio')

    if request.method == 'POST' and persona_form.is_valid() and socio_form.is_valid():
        persona = persona_form.save()
        socio = socio_form.save(commit=False)
        socio.persona = persona
        socio.save()
        messages.success(request, 'Socio creado correctamente.')
        return redirect('gimnasio:socio_lista')

    return render(
        request,
        'gimnasio/socio_form.html',
        {'persona_form': persona_form, 'socio_form': socio_form},
    )


class PlanListView(ListView):
    model = Plan
    template_name = 'gimnasio/plan_lista.html'
    context_object_name = 'planes'


class PlanCreateView(CreateView):
    model = Plan
    form_class = PlanForm
    template_name = 'gimnasio/plan_form.html'
    success_url = reverse_lazy('gimnasio:plan_lista')


class MembresiaListView(ListView):
    model = Membresia
    template_name = 'gimnasio/membresia_lista.html'
    context_object_name = 'membresias'


class MembresiaCreateView(CreateView):
    model = Membresia
    form_class = MembresiaForm
    template_name = 'gimnasio/membresia_form.html'
    success_url = reverse_lazy('gimnasio:membresia_lista')

    def form_valid(self, form):
        if not form.cleaned_data.get('fecha_fin'):
            form.instance.fecha_fin = form.cleaned_data['fecha_inicio'] + timedelta(
                days=form.cleaned_data['plan'].duracion_dias
            )
        if not form.cleaned_data.get('monto_total'):
            form.instance.monto_total = form.cleaned_data['plan'].precio

        respuesta = super().form_valid(form)
        registrar_impacto_comercial_membresia(self.object, 'venta_plan')
        messages.success(self.request, 'Membresía creada e impactada en Administración.')
        return respuesta


def validar_acceso_view(request, socio_id):
    socio = get_object_or_404(Socio, pk=socio_id)
    registro = validar_acceso_socio(socio)

    if registro.resultado == 'permitido':
        messages.success(request, f'Acceso permitido para socio {socio.numero_socio}.')
    else:
        messages.error(request, f'Acceso bloqueado para socio {socio.numero_socio}: {registro.motivo}')

    return redirect('gimnasio:socio_lista')
