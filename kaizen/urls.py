from django.contrib import admin
from django.urls import include, path

urlpatterns = [
    path('admin-django/', admin.site.urls),
    path('', include('nucleo.urls')),
    path('gimnasio/', include('gimnasio.urls')),
    path('administracion/', include('administracion.urls')),
]
