import { Routes } from '@angular/router';

export const GIMNASIO_ROUTES: Routes = [
  {
    path: '',
    loadComponent: () => import('./paginas/inicio/gimnasio-inicio.component').then(m => m.GimnasioInicioComponent)
  },
  {
    path: 'socios',
    loadComponent: () => import('./paginas/socios-listado/socios-listado.component').then(m => m.SociosListadoComponent)
  },
  {
    path: 'socios/nuevo',
    loadComponent: () => import('./paginas/socios-form/socios-form.component').then(m => m.SociosFormComponent)
  },
  {
    path: 'socios/:id/editar',
    loadComponent: () => import('./paginas/socios-form/socios-form.component').then(m => m.SociosFormComponent)
  },
  {
    path: 'planes',
    loadComponent: () => import('./paginas/planes-listado/planes-listado.component').then(m => m.PlanesListadoComponent)
  },
  {
    path: 'planes/nuevo',
    loadComponent: () => import('./paginas/planes-form/planes-form.component').then(m => m.PlanesFormComponent)
  },
  {
    path: 'planes/:id/editar',
    loadComponent: () => import('./paginas/planes-form/planes-form.component').then(m => m.PlanesFormComponent)
  },
  {
    path: 'membresias',
    loadComponent: () => import('./paginas/membresias/membresias-page.component').then(m => m.MembresiasPageComponent)
  },
  {
    path: 'acceso',
    loadComponent: () => import('./paginas/acceso/acceso-page.component').then(m => m.AccesoPageComponent)
  }
];
