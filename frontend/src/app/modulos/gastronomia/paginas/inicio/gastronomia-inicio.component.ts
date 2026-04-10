import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { RouterLink } from '@angular/router';

import { TarjetaKpiComponent } from '../../../../shared/componentes/tarjeta-kpi/tarjeta-kpi.component';
import { PageHeaderComponent } from '../../../../shared/ui/page-header/page-header.component';
import { GastronomiaApiService, DashboardGastronomiaDto } from '../../servicios/gastronomia-api.service';

@Component({
  standalone: true,
  imports: [CommonModule, RouterLink, PageHeaderComponent, TarjetaKpiComponent],
  template: `
    <app-page-header breadcrumb="Gastronomía" titulo="Gastronomía · Inicio operativo" descripcion="Control real de catálogo, recetas, pedidos y cocina" />

    <section class="kpi-grid" *ngIf="dashboard">
      <app-tarjeta-kpi titulo="Categorías" [valor]="dashboard.categorias.toString()" descripcion="maestro activo" />
      <app-tarjeta-kpi titulo="Productos" [valor]="dashboard.productos.toString()" descripcion="oferta vigente" />
      <app-tarjeta-kpi titulo="Pedidos activos" [valor]="dashboard.pedidosActivos.toString()" descripcion="cola operativa" />
      <app-tarjeta-kpi titulo="Ventas del día" [valor]="(dashboard.ventasDia | currency:'ARS') ?? ''" descripcion="entregado" />
    </section>

    <section class="panel accesos-grid">
      <a routerLink="/gastronomia/categorias">Categorías</a>
      <a routerLink="/gastronomia/productos">Productos</a>
      <a routerLink="/gastronomia/insumos">Insumos</a>
      <a routerLink="/gastronomia/recetas">Recetas</a>
      <a routerLink="/gastronomia/pedidos">Pedidos</a>
      <a routerLink="/gastronomia/cocina">Cocina</a>
    </section>
  `
})
export class GastronomiaInicioComponent {
  private readonly api = inject(GastronomiaApiService);
  dashboard?: DashboardGastronomiaDto;

  constructor() {
    this.api.obtenerDashboard().subscribe({ next: x => (this.dashboard = x) });
  }
}
