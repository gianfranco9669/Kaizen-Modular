import { Routes } from '@angular/router';

import { PlataformaDashboardComponent } from './core/layout/plataforma-dashboard/plataforma-dashboard.component';

export const appRoutes: Routes = [
  { path: '', component: PlataformaDashboardComponent },
  {
    path: 'administracion',
    loadChildren: () => import('./modulos/administracion/administracion.routes').then(m => m.ADMINISTRACION_ROUTES)
  },
  {
    path: 'gimnasio',
    loadChildren: () => import('./modulos/gimnasio/gimnasio.routes').then(m => m.GIMNASIO_ROUTES)
  },
  {
    path: 'gastronomia',
    loadChildren: () => import('./modulos/gastronomia/gastronomia.routes').then(m => m.GASTRONOMIA_ROUTES)
  }
];
