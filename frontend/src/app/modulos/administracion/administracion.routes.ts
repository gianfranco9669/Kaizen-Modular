import { Routes } from '@angular/router';

export const ADMINISTRACION_ROUTES: Routes = [
  {
    path: '',
    loadComponent: () => import('./paginas/inicio/administracion-inicio.component').then(m => m.AdministracionInicioComponent)
  },
  {
    path: 'impactos',
    loadComponent: () => import('./paginas/impactos/impactos-page.component').then(m => m.ImpactosPageComponent)
  }
];
