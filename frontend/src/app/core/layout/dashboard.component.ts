import { Component } from '@angular/core';

import { TarjetaKpiComponent } from '../../shared/componentes/tarjeta-kpi/tarjeta-kpi.component';

@Component({
  standalone: true,
  imports: [TarjetaKpiComponent],
  template: `
  <section class="panel">
    <h2>Panel general</h2>
    <p>Primer corte funcional: Gimnasio + impacto administrativo inicial.</p>
    <div class="kpis">
      <app-tarjeta-kpi titulo="Dominios" valor="3" descripcion="Gastronomía, Gimnasio y Administración"></app-tarjeta-kpi>
      <app-tarjeta-kpi titulo="Módulo activo" valor="Gimnasio" descripcion="Socios, planes, membresías y acceso"></app-tarjeta-kpi>
      <app-tarjeta-kpi titulo="Integración" valor="Inicial" descripcion="Impacto comercial en Administración"></app-tarjeta-kpi>
    </div>
  </section>
  `
})
export class DashboardComponent {}
