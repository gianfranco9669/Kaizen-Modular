from django.views.generic import TemplateView

from gimnasio.models import Membresia, Socio


class InicioView(TemplateView):
    template_name = 'nucleo/inicio.html'

    def get_context_data(self, **kwargs):
        contexto = super().get_context_data(**kwargs)
        contexto['cantidad_socios'] = Socio.objects.count()
        contexto['membresias_vigentes'] = Membresia.objects.filter(estado=Membresia.ESTADO_VIGENTE).count()
        contexto['membresias_con_deuda'] = Membresia.objects.filter(
            estado_deuda=Membresia.DEUDA_PENDIENTE
        ).count()
        return contexto
