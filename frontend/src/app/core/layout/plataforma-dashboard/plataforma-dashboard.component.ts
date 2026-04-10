import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';

import { TarjetaKpiComponent } from '../../../shared/componentes/tarjeta-kpi/tarjeta-kpi.component';
import { PageHeaderComponent } from '../../../shared/ui/page-header/page-header.component';

@Component({
  standalone: true,
  imports: [CommonModule, RouterLink, TarjetaKpiComponent, PageHeaderComponent],
  template: `
    <app-page-header
      breadcrumb="Plataforma"
      titulo="Kaizen · Panel Central"
      descripcion="Vista consolidada de dominios, actividad y alertas operativas"
    />

    <section class="kpis-grid">
      <app-tarjeta-kpi titulo="Administración" valor="Núcleo" descripcion="Control financiero y consolidación" />
      <app-tarjeta-kpi titulo="Gimnasio" valor="Activo" descripcion="Socios, planes, membresías y acceso" />
      <app-tarjeta-kpi titulo="Gastronomía" valor="Preparado" descripcion="Catálogo, producción e inventario" />
      <app-tarjeta-kpi titulo="Alertas" valor="3" descripcion="Revisión de deuda, vencimientos y caja" />
    </section>

    <section class="panel panel-grid">
      <article>
        <h3>Módulos principales</h3>
        <div class="modulos-cards">
          <a routerLink="/administracion" class="modulo-card">
            <strong>Administración</strong>
            <span>Resumen ejecutivo, impactos, tableros y control central.</span>
          </a>
          <a routerLink="/gimnasio" class="modulo-card">
            <strong>Gimnasio</strong>
            <span>Operación de socios, membresías, acceso y gestión comercial.</span>
          </a>
          <a routerLink="/gastronomia" class="modulo-card">
            <strong>Gastronomía</strong>
            <span>Base visible para catálogo, producción, inventario, ventas y caja.</span>
          </a>
        </div>
      </article>

      <article>
        <h3>Actividad reciente</h3>
        <ul class="lista-actividad">
          <li><span>Administración</span><small>Impacto comercial registrado hace 5 min.</small></li>
          <li><span>Gimnasio</span><small>Alta de membresía con deuda pendiente.</small></li>
          <li><span>Gastronomía</span><small>Estructura inicial de módulo visible para el equipo.</small></li>
        </ul>
      </article>

      <article>
        <h3>Accesos rápidos</h3>
        <div class="quick-actions">
          <a routerLink="/gimnasio/socios/nuevo">Nuevo socio</a>
          <a routerLink="/gimnasio/membresias">Gestionar membresías</a>
          <a routerLink="/administracion/impactos">Ver impactos</a>
          <a routerLink="/gastronomia">Ir a Gastronomía</a>
        </div>
      </article>
    </section>
  `
})
export class PlataformaDashboardComponent {}
