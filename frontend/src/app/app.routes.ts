import { Routes } from '@angular/router';

import { DashboardComponent } from './core/layout/dashboard.component';
import { ImpactosPageComponent } from './modulos/administracion/paginas/impactos/impactos-page.component';
import { AccesoPageComponent } from './modulos/gimnasio/paginas/acceso/acceso-page.component';
import { MembresiasPageComponent } from './modulos/gimnasio/paginas/membresias/membresias-page.component';
import { PlanesPageComponent } from './modulos/gimnasio/paginas/planes/planes-page.component';
import { SociosPageComponent } from './modulos/gimnasio/paginas/socios/socios-page.component';

export const appRoutes: Routes = [
  { path: '', component: DashboardComponent },
  { path: 'gimnasio/socios', component: SociosPageComponent },
  { path: 'gimnasio/planes', component: PlanesPageComponent },
  { path: 'gimnasio/membresias', component: MembresiasPageComponent },
  { path: 'gimnasio/acceso', component: AccesoPageComponent },
  { path: 'administracion/impactos', component: ImpactosPageComponent }
];
