import { Routes } from '@angular/router';

export const GASTRONOMIA_ROUTES: Routes = [
  {
    path: '',
    loadComponent: () => import('./paginas/inicio/gastronomia-inicio.component').then(m => m.GastronomiaInicioComponent)
  },
  {
    path: ':seccion',
    loadComponent: () => import('./paginas/seccion/gastronomia-seccion.component').then(m => m.GastronomiaSeccionComponent)
  }
];
