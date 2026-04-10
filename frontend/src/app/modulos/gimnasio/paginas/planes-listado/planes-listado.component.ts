import { CommonModule } from '@angular/common';
import { Component, OnInit, TemplateRef, ViewChild, inject } from '@angular/core';
import { RouterLink } from '@angular/router';

import { EmptyStateComponent } from '../../../../shared/ui/empty-state/empty-state.component';
import { FiltrosBarraComponent } from '../../../../shared/ui/filtros-barra/filtros-barra.component';
import { LoadingStateComponent } from '../../../../shared/ui/loading-state/loading-state.component';
import { PaginadorComponent } from '../../../../shared/ui/paginador/paginador.component';
import { PageHeaderComponent } from '../../../../shared/ui/page-header/page-header.component';
import { StatusBadgeComponent } from '../../../../shared/ui/status-badge/status-badge.component';
import { TablaAvanzadaComponent } from '../../../../shared/ui/tabla-avanzada/tabla-avanzada.component';
import { GimnasioApiService, Plan } from '../../servicios/gimnasio-api.service';

@Component({
  standalone: true,
  imports: [
    CommonModule,
    RouterLink,
    PageHeaderComponent,
    FiltrosBarraComponent,
    TablaAvanzadaComponent,
    EmptyStateComponent,
    LoadingStateComponent,
    PaginadorComponent,
    StatusBadgeComponent
  ],
  templateUrl: './planes-listado.component.html'
})
export class PlanesListadoComponent implements OnInit {
  private readonly api = inject(GimnasioApiService);
  @ViewChild('filaTpl', { static: true }) filaTpl!: TemplateRef<unknown>;

  cargando = true;
  filtro = '';
  planes: Plan[] = [];
  pagina = 1;
  porPagina = 8;

  get planosFiltrados(): Plan[] {
    const f = this.filtro.toLowerCase().trim();
    return this.planes.filter(p => `${p.nombre} ${p.descripcion}`.toLowerCase().includes(f));
  }

  get paginados(): Plan[] {
    const inicio = (this.pagina - 1) * this.porPagina;
    return this.planosFiltrados.slice(inicio, inicio + this.porPagina);
  }

  get totalPaginas(): number {
    return Math.max(1, Math.ceil(this.planosFiltrados.length / this.porPagina));
  }

  ngOnInit(): void {
    this.api.listarPlanes().subscribe(data => {
      this.planes = data;
      this.cargando = false;
    });
  }
}
