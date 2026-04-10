from django.urls import path

from .views import ImpactoComercialListView

app_name = 'administracion'

urlpatterns = [
    path('impacto-comercial/', ImpactoComercialListView.as_view(), name='impacto_comercial_lista'),
]
