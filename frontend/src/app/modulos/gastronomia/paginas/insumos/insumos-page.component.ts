import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';

import { PageHeaderComponent } from '../../../../shared/ui/page-header/page-header.component';
import { GastronomiaApiService, InsumoGastronomiaDto } from '../../servicios/gastronomia-api.service';

@Component({
  standalone: true,
  imports: [CommonModule, FormsModule, PageHeaderComponent],
  template: `
    <app-page-header breadcrumb="Gastronomía / Insumos" titulo="Insumos de gastronomía" descripcion="Stock, costos y mínimos" />
    <section class="panel formulario-grid-5">
      <input [(ngModel)]="nombre" placeholder="Nombre" />
      <input [(ngModel)]="unidad" placeholder="Unidad" />
      <input [(ngModel)]="costoUnitario" type="number" step="0.01" placeholder="Costo" />
      <input [(ngModel)]="stockActual" type="number" step="0.001" placeholder="Stock" />
      <input [(ngModel)]="stockMinimo" type="number" step="0.001" placeholder="Mínimo" />
      <button (click)="crear()">Agregar</button>
    </section>
    <section class="panel">
      <table class="tabla-pro"><thead><tr><th>Insumo</th><th>Unidad</th><th>Stock</th><th>Mínimo</th></tr></thead>
      <tbody><tr *ngFor="let i of insumos"><td>{{ i.nombre }}</td><td>{{ i.unidad }}</td><td>{{ i.stockActual }}</td><td>{{ i.stockMinimo }}</td></tr></tbody></table>
    </section>
  `
})
export class InsumosPageComponent {
  private readonly api = inject(GastronomiaApiService);
  insumos: InsumoGastronomiaDto[] = [];
  nombre = '';
  unidad = 'unidad';
  costoUnitario = 0;
  stockActual = 0;
  stockMinimo = 0;

  constructor() { this.cargar(); }
  private cargar(): void { this.api.listarInsumos().subscribe({ next: x => (this.insumos = x) }); }

  crear(): void {
    if (!this.nombre.trim()) return;
    this.api.crearInsumo({ nombre: this.nombre, unidad: this.unidad, costoUnitario: this.costoUnitario, stockActual: this.stockActual, stockMinimo: this.stockMinimo }).subscribe({ next: () => { this.nombre = ''; this.cargar(); } });
  }
}
