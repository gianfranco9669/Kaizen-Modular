import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';

import { PageHeaderComponent } from '../../../../shared/ui/page-header/page-header.component';
import { GastronomiaApiService, PedidoGastronomiaDto, ProductoGastronomiaDto } from '../../servicios/gastronomia-api.service';

@Component({
  standalone: true,
  imports: [CommonModule, FormsModule, PageHeaderComponent],
  template: `
    <app-page-header breadcrumb="Gastronomía / Pedidos" titulo="Pedidos gastronómicos" descripcion="Canales salón, mostrador y delivery" />
    <section class="panel formulario-grid">
      <select [(ngModel)]="canal"><option value="salon">Salón</option><option value="mostrador">Mostrador</option><option value="delivery">Delivery</option></select>
      <input [(ngModel)]="cliente" placeholder="Cliente" />
      <select [(ngModel)]="productoGastronomiaId"><option value="">Producto</option><option *ngFor="let p of productos" [value]="p.id">{{ p.nombre }}</option></select>
      <input [(ngModel)]="cantidad" type="number" min="1" />
      <button (click)="agregarItem()">Agregar item</button>
      <button class="primario" (click)="crearPedido()">Crear pedido</button>
    </section>
    <section class="panel">
      <table class="tabla-pro"><thead><tr><th>Fecha</th><th>Canal</th><th>Cliente</th><th>Estado</th><th>Total</th><th>Acción</th></tr></thead>
      <tbody><tr *ngFor="let p of pedidos">
        <td>{{ p.fechaPedidoUtc | date:'short' }}</td><td>{{ p.canal }}</td><td>{{ p.cliente }}</td><td><span class="badge info">{{ p.estado }}</span></td><td>{{ p.total | currency:'ARS' }}</td>
        <td><button (click)="avanzar(p)">Avanzar estado</button></td>
      </tr></tbody></table>
    </section>
  `
})
export class PedidosPageComponent {
  private readonly api = inject(GastronomiaApiService);
  pedidos: PedidoGastronomiaDto[] = [];
  productos: ProductoGastronomiaDto[] = [];
  itemsBorrador: { productoGastronomiaId: string; cantidad: number }[] = [];

  canal = 'salon';
  cliente = '';
  productoGastronomiaId = '';
  cantidad = 1;

  constructor() {
    this.api.listarProductos().subscribe({ next: x => (this.productos = x) });
    this.cargar();
  }

  private cargar(): void { this.api.listarPedidos().subscribe({ next: x => (this.pedidos = x) }); }

  agregarItem(): void {
    if (!this.productoGastronomiaId || this.cantidad <= 0) return;
    this.itemsBorrador.push({ productoGastronomiaId: this.productoGastronomiaId, cantidad: this.cantidad });
    this.productoGastronomiaId = '';
    this.cantidad = 1;
  }

  crearPedido(): void {
    if (this.itemsBorrador.length === 0) return;
    this.api.crearPedido({ canal: this.canal, cliente: this.cliente, items: this.itemsBorrador }).subscribe({ next: () => { this.itemsBorrador = []; this.cliente = ''; this.cargar(); } });
  }

  avanzar(pedido: PedidoGastronomiaDto): void {
    const mapa: Record<string, string> = { nuevo: 'en_preparacion', en_preparacion: 'listo', listo: 'entregado' };
    const siguiente = mapa[pedido.estado];
    if (!siguiente) return;
    this.api.cambiarEstadoPedido(pedido.id, siguiente).subscribe({ next: () => this.cargar() });
  }
}
