import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';

import { PageHeaderComponent } from '../../../../shared/ui/page-header/page-header.component';

@Component({
  standalone: true,
  imports: [CommonModule, RouterLink, PageHeaderComponent],
  template: `
    <app-page-header
      breadcrumb="Gastronomía / {{ seccion | titlecase }}"
      [titulo]="'Gastronomía · ' + (seccion | titlecase)"
      descripcion="Sección visible y lista para implementación funcional del dominio"
      accionTexto="Volver al módulo"
      accionRuta="/gastronomia"
    />

    <section class="panel">
      <h3>Estado de la sección</h3>
      <p>Esta sección está preparada en la navegación estructural del producto y queda pendiente de implementación funcional en siguientes iteraciones.</p>
    </section>
  `
})
export class GastronomiaSeccionComponent {
  private readonly route = inject(ActivatedRoute);
  seccion = this.route.snapshot.paramMap.get('seccion') ?? 'seccion';
}
