from django.urls import path

from .views import (
    MembresiaCreateView,
    MembresiaListView,
    PlanCreateView,
    PlanListView,
    SocioListView,
    socio_crear,
    validar_acceso_view,
)

app_name = 'gimnasio'

urlpatterns = [
    path('socios/', SocioListView.as_view(), name='socio_lista'),
    path('socios/nuevo/', socio_crear, name='socio_crear'),
    path('socios/<int:socio_id>/validar-acceso/', validar_acceso_view, name='validar_acceso'),
    path('planes/', PlanListView.as_view(), name='plan_lista'),
    path('planes/nuevo/', PlanCreateView.as_view(), name='plan_crear'),
    path('membresias/', MembresiaListView.as_view(), name='membresia_lista'),
    path('membresias/nueva/', MembresiaCreateView.as_view(), name='membresia_crear'),
]
