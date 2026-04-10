import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-loading-state',
  standalone: true,
  template: `<div class="loading">{{ texto }}</div>`
})
export class LoadingStateComponent {
  @Input() texto = 'Cargando información...';
}
