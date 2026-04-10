import { Routes } from '@angular/router';

export const GASTRONOMIA_ROUTES: Routes = [
  { path: '', loadComponent: () => import('./paginas/inicio/gastronomia-inicio.component').then(m => m.GastronomiaInicioComponent) },
  { path: 'categorias', loadComponent: () => import('./paginas/categorias/categorias-page.component').then(m => m.CategoriasPageComponent) },
  { path: 'productos', loadComponent: () => import('./paginas/productos/productos-page.component').then(m => m.ProductosPageComponent) },
  { path: 'insumos', loadComponent: () => import('./paginas/insumos/insumos-page.component').then(m => m.InsumosPageComponent) },
  { path: 'recetas', loadComponent: () => import('./paginas/recetas/recetas-page.component').then(m => m.RecetasPageComponent) },
  { path: 'pedidos', loadComponent: () => import('./paginas/pedidos/pedidos-page.component').then(m => m.PedidosPageComponent) },
  { path: 'cocina', loadComponent: () => import('./paginas/cocina/cocina-page.component').then(m => m.CocinaPageComponent) }
];
