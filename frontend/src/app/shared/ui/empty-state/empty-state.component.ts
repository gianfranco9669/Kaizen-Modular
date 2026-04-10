import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-empty-state',
  standalone: true,
  template: `
    <div class="empty-state">
      <h3>{{ titulo }}</h3>
      <p>{{ mensaje }}</p>
    </div>
  `
})
export class EmptyStateComponent {
  @Input() titulo = 'Sin resultados';
  @Input() mensaje = 'No se encontraron registros con los filtros seleccionados.';
}
