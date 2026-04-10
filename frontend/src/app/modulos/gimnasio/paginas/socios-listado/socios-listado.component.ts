import { CommonModule } from '@angular/common';
import { Component, OnInit, TemplateRef, ViewChild, inject } from '@angular/core';
import { RouterLink } from '@angular/router';

import { EmptyStateComponent } from '../../../../shared/ui/empty-state/empty-state.component';
import { FiltrosBarraComponent } from '../../../../shared/ui/filtros-barra/filtros-barra.component';
import { LoadingStateComponent } from '../../../../shared/ui/loading-state/loading-state.component';
import { PaginadorComponent } from '../../../../shared/ui/paginador/paginador.component';
import { PageHeaderComponent } from '../../../../shared/ui/page-header/page-header.component';
import { TablaAvanzadaComponent } from '../../../../shared/ui/tabla-avanzada/tabla-avanzada.component';
import { GimnasioApiService, Socio } from '../../servicios/gimnasio-api.service';

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
    PaginadorComponent
  ],
  templateUrl: './socios-listado.component.html'
})
export class SociosListadoComponent implements OnInit {
  private readonly api = inject(GimnasioApiService);

  @ViewChild('filaTpl', { static: true }) filaTpl!: TemplateRef<unknown>;

  cargando = true;
  error = '';
  filtro = '';
  pagina = 1;
  porPagina = 8;
  socios: Socio[] = [];

  get sociosFiltrados(): Socio[] {
    const f = this.filtro.toLowerCase().trim();
    return this.socios.filter(s =>
      `${s.numeroSocio} ${s.nombre} ${s.apellido} ${s.documento}`.toLowerCase().includes(f)
    );
  }

  get sociosPaginados(): Socio[] {
    const inicio = (this.pagina - 1) * this.porPagina;
    return this.sociosFiltrados.slice(inicio, inicio + this.porPagina);
  }

  get totalPaginas(): number {
    return Math.max(1, Math.ceil(this.sociosFiltrados.length / this.porPagina));
  }

  ngOnInit(): void {
    this.recargar();
  }

  recargar(): void {
    this.cargando = true;
    this.error = '';
    this.api.listarSocios().subscribe({
      next: data => {
        this.socios = data;
        this.cargando = false;
      },
      error: () => {
        this.error = 'No se pudo cargar el listado de socios.';
        this.cargando = false;
      }
    });
  }
}
