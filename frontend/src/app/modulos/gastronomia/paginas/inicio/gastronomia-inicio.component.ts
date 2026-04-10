import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';

import { PageHeaderComponent } from '../../../../shared/ui/page-header/page-header.component';

@Component({
  standalone: true,
  imports: [RouterLink, PageHeaderComponent],
  template: `
    <app-page-header
      breadcrumb="Gastronomía"
      titulo="Gastronomía · Panel del módulo"
      descripcion="Módulo visible y preparado para implementación operativa"
    />

    <section class="panel placeholders-grid">
      <a routerLink="/gastronomia/catalogo" class="placeholder-card"><strong>Catálogo</strong><span>Productos, insumos y listas de precios.</span></a>
      <a routerLink="/gastronomia/produccion" class="placeholder-card"><strong>Producción</strong><span>Recetas, lotes y rendimiento.</span></a>
      <a routerLink="/gastronomia/inventario" class="placeholder-card"><strong>Inventario</strong><span>Stock, movimientos y mermas.</span></a>
      <a routerLink="/gastronomia/ventas" class="placeholder-card"><strong>Ventas</strong><span>Pedidos, canales y trazabilidad comercial.</span></a>
      <a routerLink="/gastronomia/cocina" class="placeholder-card"><strong>Cocina</strong><span>Cola de preparación y despacho.</span></a>
      <a routerLink="/gastronomia/caja" class="placeholder-card"><strong>Caja</strong><span>Apertura, cierres y arqueos.</span></a>
    </section>
  `
})
export class GastronomiaInicioComponent {}
