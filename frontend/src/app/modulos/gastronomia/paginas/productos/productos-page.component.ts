import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';

import { PageHeaderComponent } from '../../../../shared/ui/page-header/page-header.component';
import { CategoriaGastronomiaDto, GastronomiaApiService, ProductoGastronomiaDto } from '../../servicios/gastronomia-api.service';

@Component({
  standalone: true,
  imports: [CommonModule, FormsModule, PageHeaderComponent],
  template: `
    <app-page-header breadcrumb="Gastronomía / Productos" titulo="Productos de gastronomía" descripcion="Productos con receta opcional" />
    <section class="panel formulario-grid">
      <input [(ngModel)]="nombre" placeholder="Nombre" />
      <select [(ngModel)]="categoriaGastronomiaId"><option value="">Categoría</option><option *ngFor="let c of categorias" [value]="c.id">{{ c.nombre }}</option></select>
      <input [(ngModel)]="precioVenta" type="number" min="0" step="0.01" placeholder="Precio" />
      <button (click)="crear()">Agregar</button>
    </section>
    <section class="panel">
      <table class="tabla-pro"><thead><tr><th>Producto</th><th>Categoría</th><th>Precio</th></tr></thead>
      <tbody><tr *ngFor="let p of productos"><td>{{ p.nombre }}</td><td>{{ p.categoriaNombre }}</td><td>{{ p.precioVenta | currency:'ARS' }}</td></tr></tbody></table>
    </section>
  `
})
export class ProductosPageComponent {
  private readonly api = inject(GastronomiaApiService);
  categorias: CategoriaGastronomiaDto[] = [];
  productos: ProductoGastronomiaDto[] = [];
  nombre = '';
  categoriaGastronomiaId = '';
  precioVenta = 0;

  constructor() { this.cargarTodo(); }
  private cargarTodo(): void { this.api.listarCategorias().subscribe({ next: x => (this.categorias = x) }); this.api.listarProductos().subscribe({ next: x => (this.productos = x) }); }

  crear(): void {
    if (!this.nombre.trim() || !this.categoriaGastronomiaId || this.precioVenta <= 0) return;
    this.api.crearProducto({ nombre: this.nombre, categoriaGastronomiaId: this.categoriaGastronomiaId, precioVenta: this.precioVenta }).subscribe({ next: () => { this.nombre = ''; this.precioVenta = 0; this.cargarTodo(); } });
  }
}
