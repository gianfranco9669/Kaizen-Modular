import { CommonModule } from '@angular/common';
import { Component, OnInit, inject } from '@angular/core';

import { EmptyStateComponent } from '../../../../shared/ui/empty-state/empty-state.component';
import { FiltrosBarraComponent } from '../../../../shared/ui/filtros-barra/filtros-barra.component';
import { LoadingStateComponent } from '../../../../shared/ui/loading-state/loading-state.component';
import { PageHeaderComponent } from '../../../../shared/ui/page-header/page-header.component';
import { TarjetaKpiComponent } from '../../../../shared/componentes/tarjeta-kpi/tarjeta-kpi.component';
import { AdministracionApiService, ImpactoComercial } from '../../servicios/administracion-api.service';

@Component({
  standalone: true,
  imports: [CommonModule, PageHeaderComponent, TarjetaKpiComponent, FiltrosBarraComponent, LoadingStateComponent, EmptyStateComponent],
  templateUrl: './impactos-page.component.html'
})
export class ImpactosPageComponent implements OnInit {
  private readonly api = inject(AdministracionApiService);

  impactos: ImpactoComercial[] = [];
  cargando = true;
  filtro = '';

  get impactosFiltrados(): ImpactoComercial[] {
    const f = this.filtro.toLowerCase();
    return this.impactos.filter(i => `${i.moduloOrigen} ${i.tipoOperacion} ${i.referenciaExterna}`.toLowerCase().includes(f));
  }

  get totalMonto(): number {
    return this.impactosFiltrados.reduce((acc, x) => acc + x.monto, 0);
  }

  ngOnInit(): void {
    this.api.listarImpactos().subscribe(data => {
      this.impactos = data;
      this.cargando = false;
    });
  }
}
