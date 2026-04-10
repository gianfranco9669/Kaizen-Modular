import { Routes } from '@angular/router';

export const ADMINISTRACION_ROUTES: Routes = [
  {
    path: 'impactos',
    loadComponent: () => import('./paginas/impactos/impactos-page.component').then(m => m.ImpactosPageComponent)
  }
];
