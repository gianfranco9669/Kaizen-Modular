import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';

import { TarjetaKpiComponent } from '../../../../shared/componentes/tarjeta-kpi/tarjeta-kpi.component';
import { PageHeaderComponent } from '../../../../shared/ui/page-header/page-header.component';

@Component({
  standalone: true,
  imports: [RouterLink, PageHeaderComponent, TarjetaKpiComponent],
  template: `
    <app-page-header
      breadcrumb="Administración"
      titulo="Administración Central"
      descripcion="Núcleo de control consolidado y soporte administrativo"
      accionTexto="Ver impactos"
      accionRuta="/administracion/impactos"
    />

    <section class="kpis-grid">
      <app-tarjeta-kpi titulo="Impactos" valor="Consolidado" descripcion="Eventos comerciales de módulos" />
      <app-tarjeta-kpi titulo="Tesorería" valor="Próximo" descripcion="Base preparada para siguiente corte" />
      <app-tarjeta-kpi titulo="Contabilidad" valor="Próximo" descripcion="Plan de cuentas y asientos" />
    </section>

    <section class="panel subnav">
      <a routerLink="/administracion/impactos">Impactos comerciales</a>
      <a class="disabled">Caja y bancos (próximo)</a>
      <a class="disabled">Cuentas corrientes (próximo)</a>
      <a class="disabled">Indicadores y tableros (próximo)</a>
    </section>
  `
})
export class AdministracionInicioComponent {}
