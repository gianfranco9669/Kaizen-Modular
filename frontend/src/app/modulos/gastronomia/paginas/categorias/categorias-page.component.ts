import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';

import { PageHeaderComponent } from '../../../../shared/ui/page-header/page-header.component';
import { CategoriaGastronomiaDto, GastronomiaApiService } from '../../servicios/gastronomia-api.service';

@Component({
  standalone: true,
  imports: [CommonModule, FormsModule, PageHeaderComponent],
  template: `
    <app-page-header breadcrumb="Gastronomía / Categorías" titulo="Categorías de gastronomía" descripcion="Clasificación del catálogo" />
    <section class="panel formulario-grid">
      <input [(ngModel)]="nombre" placeholder="Nombre" />
      <input [(ngModel)]="descripcion" placeholder="Descripción" />
      <button (click)="crear()">Agregar</button>
    </section>
    <section class="panel">
      <table class="tabla-pro"><thead><tr><th>Nombre</th><th>Descripción</th></tr></thead>
      <tbody><tr *ngFor="let c of categorias"><td>{{ c.nombre }}</td><td>{{ c.descripcion }}</td></tr></tbody></table>
    </section>
  `
})
export class CategoriasPageComponent {
  private readonly api = inject(GastronomiaApiService);
  categorias: CategoriaGastronomiaDto[] = [];
  nombre = '';
  descripcion = '';

  constructor() { this.cargar(); }
  private cargar(): void { this.api.listarCategorias().subscribe({ next: x => (this.categorias = x) }); }
  crear(): void {
    if (!this.nombre.trim()) return;
    this.api.crearCategoria({ nombre: this.nombre, descripcion: this.descripcion }).subscribe({ next: () => { this.nombre = ''; this.descripcion = ''; this.cargar(); } });
  }
}
