import { Component } from '@angular/core';

import { TarjetaKpiComponent } from '../../shared/componentes/tarjeta-kpi/tarjeta-kpi.component';
import { PageHeaderComponent } from '../../shared/ui/page-header/page-header.component';

@Component({
  standalone: true,
  imports: [TarjetaKpiComponent, PageHeaderComponent],
  template: `
  <app-page-header
    breadcrumb="Inicio"
    titulo="Panel Operativo"
    descripcion="Resumen ejecutivo de la operación actual"
  />
  <section class="panel">
    <div class="kpis">
      <app-tarjeta-kpi titulo="Dominios" valor="3" descripcion="Gastronomía, Gimnasio, Administración"></app-tarjeta-kpi>
      <app-tarjeta-kpi titulo="Corte activo" valor="Gimnasio" descripcion="Alta, edición, membresías y accesos"></app-tarjeta-kpi>
      <app-tarjeta-kpi titulo="Integración" valor="OK" descripcion="Impactos administrativos disponibles"></app-tarjeta-kpi>
    </div>
  </section>
  `
})
export class DashboardComponent {}
