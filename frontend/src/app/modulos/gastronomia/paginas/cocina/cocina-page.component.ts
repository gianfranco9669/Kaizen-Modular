import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';

import { PageHeaderComponent } from '../../../../shared/ui/page-header/page-header.component';
import { GastronomiaApiService, PedidoGastronomiaDto } from '../../servicios/gastronomia-api.service';

@Component({
  standalone: true,
  imports: [CommonModule, PageHeaderComponent],
  template: `
    <app-page-header breadcrumb="Gastronomía / Cocina" titulo="Cocina · Cola de pedidos" descripcion="Monitoreo operativo de preparación" />
    <section class="panel">
      <table class="tabla-pro"><thead><tr><th>Hora</th><th>Cliente</th><th>Canal</th><th>Estado</th><th>Items</th></tr></thead>
      <tbody><tr *ngFor="let pedido of cola"><td>{{ pedido.fechaPedidoUtc | date:'shortTime' }}</td><td>{{ pedido.cliente }}</td><td>{{ pedido.canal }}</td><td>{{ pedido.estado }}</td><td>{{ pedido.items.length }}</td></tr></tbody></table>
    </section>
  `
})
export class CocinaPageComponent {
  private readonly api = inject(GastronomiaApiService);
  cola: PedidoGastronomiaDto[] = [];

  constructor() {
    this.api.colaCocina().subscribe({ next: x => (this.cola = x) });
  }
}
