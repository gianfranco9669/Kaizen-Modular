from django.views.generic import ListView

from .models import RegistroImpactoComercial


class ImpactoComercialListView(ListView):
    model = RegistroImpactoComercial
    template_name = 'administracion/impacto_comercial_lista.html'
    context_object_name = 'registros'
    paginate_by = 20
