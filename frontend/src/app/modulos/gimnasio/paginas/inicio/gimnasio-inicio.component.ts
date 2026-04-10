import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';

import { TarjetaKpiComponent } from '../../../../shared/componentes/tarjeta-kpi/tarjeta-kpi.component';
import { PageHeaderComponent } from '../../../../shared/ui/page-header/page-header.component';

@Component({
  standalone: true,
  imports: [RouterLink, PageHeaderComponent, TarjetaKpiComponent],
  template: `
    <app-page-header
      breadcrumb="Gimnasio"
      titulo="Gimnasio · Panel operativo"
      descripcion="Acceso central para socios, planes, membresías y control de ingreso"
    />

    <section class="kpis-grid">
      <app-tarjeta-kpi titulo="Socios" valor="Operativo" descripcion="Listado + alta/edición" />
      <app-tarjeta-kpi titulo="Membresías" valor="Operativo" descripcion="Vigencia y deuda" />
      <app-tarjeta-kpi titulo="Acceso" valor="Operativo" descripcion="Validación por número/doc" />
    </section>

    <section class="panel subnav">
      <a routerLink="/gimnasio/socios">Socios</a>
      <a routerLink="/gimnasio/planes">Planes</a>
      <a routerLink="/gimnasio/membresias">Membresías</a>
      <a routerLink="/gimnasio/acceso">Control de acceso</a>
    </section>
  `
})
export class GimnasioInicioComponent {}
