import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-status-badge',
  standalone: true,
  imports: [CommonModule],
  template: `<span class="badge" [ngClass]="tipo">{{ texto }}</span>`
})
export class StatusBadgeComponent {
  @Input() texto = '';
  @Input() tipo: 'ok' | 'alerta' | 'error' | 'info' = 'info';
}
