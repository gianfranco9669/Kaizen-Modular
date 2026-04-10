import { Routes } from '@angular/router';

import { DashboardComponent } from './core/layout/dashboard.component';

export const appRoutes: Routes = [
  { path: '', component: DashboardComponent },
  {
    path: 'gimnasio',
    loadChildren: () => import('./modulos/gimnasio/gimnasio.routes').then(m => m.GIMNASIO_ROUTES)
  },
  {
    path: 'administracion',
    loadChildren: () => import('./modulos/administracion/administracion.routes').then(m => m.ADMINISTRACION_ROUTES)
  }
];
