import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';

import { environment } from '../../../../environments/environment';

export interface CategoriaGastronomiaDto { id: string; nombre: string; descripcion: string; activa: boolean; }
export interface ProductoGastronomiaDto { id: string; nombre: string; categoriaGastronomiaId: string; categoriaNombre: string; precioVenta: number; activo: boolean; }
export interface InsumoGastronomiaDto { id: string; nombre: string; unidad: string; costoUnitario: number; stockActual: number; stockMinimo: number; activo: boolean; }
export interface RecetaItemDto { insumoGastronomiaId: string; insumoNombre: string; cantidad: number; unidad: string; }
export interface RecetaProductoDto { recetaProductoId: string; productoGastronomiaId: string; productoNombre: string; items: RecetaItemDto[]; costoEstimado: number; }
export interface PedidoItemGastronomiaDto { productoGastronomiaId: string; productoNombre: string; cantidad: number; precioUnitario: number; subtotal: number; }
export interface PedidoGastronomiaDto { id: string; canal: string; estado: string; cliente: string; observaciones: string; fechaPedidoUtc: string; total: number; items: PedidoItemGastronomiaDto[]; }
export interface MovimientoStockGastronomiaDto { id: string; insumoGastronomiaId: string; insumoNombre: string; tipoMovimiento: string; cantidad: number; referencia: string; fechaMovimientoUtc: string; }
export interface DashboardGastronomiaDto { categorias: number; productos: number; insumos: number; pedidosActivos: number; ventasDia: number; insumosBajoMinimo: number; }

@Injectable({ providedIn: 'root' })
export class GastronomiaApiService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = `${environment.apiBaseUrl}/gastronomia`;

  obtenerDashboard(): Observable<DashboardGastronomiaDto> { return this.http.get<DashboardGastronomiaDto>(`${this.baseUrl}/dashboard`); }
  listarCategorias(): Observable<CategoriaGastronomiaDto[]> { return this.http.get<CategoriaGastronomiaDto[]>(`${this.baseUrl}/categorias`); }
  crearCategoria(payload: { nombre: string; descripcion?: string }): Observable<CategoriaGastronomiaDto> { return this.http.post<CategoriaGastronomiaDto>(`${this.baseUrl}/categorias`, payload); }

  listarProductos(): Observable<ProductoGastronomiaDto[]> { return this.http.get<ProductoGastronomiaDto[]>(`${this.baseUrl}/productos`); }
  crearProducto(payload: { nombre: string; categoriaGastronomiaId: string; precioVenta: number }): Observable<ProductoGastronomiaDto> { return this.http.post<ProductoGastronomiaDto>(`${this.baseUrl}/productos`, payload); }

  listarInsumos(): Observable<InsumoGastronomiaDto[]> { return this.http.get<InsumoGastronomiaDto[]>(`${this.baseUrl}/insumos`); }
  crearInsumo(payload: { nombre: string; unidad: string; costoUnitario: number; stockActual: number; stockMinimo: number }): Observable<InsumoGastronomiaDto> { return this.http.post<InsumoGastronomiaDto>(`${this.baseUrl}/insumos`, payload); }

  obtenerRecetaPorProducto(productoId: string): Observable<RecetaProductoDto> { return this.http.get<RecetaProductoDto>(`${this.baseUrl}/recetas/producto/${productoId}`); }
  guardarReceta(payload: { productoGastronomiaId: string; items: { insumoGastronomiaId: string; cantidad: number }[] }): Observable<RecetaProductoDto> { return this.http.post<RecetaProductoDto>(`${this.baseUrl}/recetas`, payload); }

  listarPedidos(): Observable<PedidoGastronomiaDto[]> { return this.http.get<PedidoGastronomiaDto[]>(`${this.baseUrl}/pedidos`); }
  crearPedido(payload: { canal: string; cliente?: string; observaciones?: string; items: { productoGastronomiaId: string; cantidad: number }[] }): Observable<PedidoGastronomiaDto> { return this.http.post<PedidoGastronomiaDto>(`${this.baseUrl}/pedidos`, payload); }
  cambiarEstadoPedido(pedidoId: string, estado: string): Observable<PedidoGastronomiaDto> { return this.http.put<PedidoGastronomiaDto>(`${this.baseUrl}/pedidos/${pedidoId}/estado`, { estado }); }

  colaCocina(): Observable<PedidoGastronomiaDto[]> { return this.http.get<PedidoGastronomiaDto[]>(`${this.baseUrl}/cocina/cola`); }
  movimientosStock(): Observable<MovimientoStockGastronomiaDto[]> { return this.http.get<MovimientoStockGastronomiaDto[]>(`${this.baseUrl}/stock/movimientos`); }
}
