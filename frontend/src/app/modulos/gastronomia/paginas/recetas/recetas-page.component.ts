import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';

import { PageHeaderComponent } from '../../../../shared/ui/page-header/page-header.component';
import { GastronomiaApiService, InsumoGastronomiaDto, ProductoGastronomiaDto, RecetaProductoDto } from '../../servicios/gastronomia-api.service';

@Component({
  standalone: true,
  imports: [CommonModule, FormsModule, PageHeaderComponent],
  template: `
    <app-page-header breadcrumb="Gastronomía / Recetas" titulo="Recetas por producto" descripcion="Composición e impacto de insumos" />
    <section class="panel formulario-grid">
      <select [(ngModel)]="productoGastronomiaId" (change)="cargarReceta()"><option value="">Producto</option><option *ngFor="let p of productos" [value]="p.id">{{ p.nombre }}</option></select>
      <select [(ngModel)]="insumoGastronomiaId"><option value="">Insumo</option><option *ngFor="let i of insumos" [value]="i.id">{{ i.nombre }}</option></select>
      <input [(ngModel)]="cantidad" type="number" min="0" step="0.001" placeholder="Cantidad" />
      <button (click)="agregarItem()">Agregar item</button>
      <button class="primario" (click)="guardarReceta()">Guardar receta</button>
    </section>
    <section class="panel" *ngIf="receta">
      <h3>{{ receta.productoNombre }}</h3>
      <p>Costo estimado: {{ receta.costoEstimado | currency:'ARS' }}</p>
      <table class="tabla-pro"><thead><tr><th>Insumo</th><th>Cantidad</th></tr></thead>
      <tbody><tr *ngFor="let i of receta.items"><td>{{ i.insumoNombre }}</td><td>{{ i.cantidad }} {{ i.unidad }}</td></tr></tbody></table>
    </section>
  `
})
export class RecetasPageComponent {
  private readonly api = inject(GastronomiaApiService);
  productos: ProductoGastronomiaDto[] = [];
  insumos: InsumoGastronomiaDto[] = [];
  receta?: RecetaProductoDto;

  productoGastronomiaId = '';
  insumoGastronomiaId = '';
  cantidad = 0;
  itemsBorrador: { insumoGastronomiaId: string; cantidad: number }[] = [];

  constructor() {
    this.api.listarProductos().subscribe({ next: x => (this.productos = x) });
    this.api.listarInsumos().subscribe({ next: x => (this.insumos = x) });
  }

  agregarItem(): void {
    if (!this.insumoGastronomiaId || this.cantidad <= 0) return;
    this.itemsBorrador.push({ insumoGastronomiaId: this.insumoGastronomiaId, cantidad: this.cantidad });
    this.insumoGastronomiaId = '';
    this.cantidad = 0;
  }

  guardarReceta(): void {
    if (!this.productoGastronomiaId || this.itemsBorrador.length === 0) return;
    this.api.guardarReceta({ productoGastronomiaId: this.productoGastronomiaId, items: this.itemsBorrador }).subscribe({
      next: x => { this.receta = x; this.itemsBorrador = []; }
    });
  }

  cargarReceta(): void {
    if (!this.productoGastronomiaId) return;
    this.api.obtenerRecetaPorProducto(this.productoGastronomiaId).subscribe({ next: x => (this.receta = x), error: () => (this.receta = undefined) });
  }
}
