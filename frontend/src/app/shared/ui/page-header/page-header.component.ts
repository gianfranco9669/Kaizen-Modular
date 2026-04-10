import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-page-header',
  standalone: true,
  imports: [CommonModule, RouterLink],
  template: `
    <header class="page-header">
      <div>
        <small class="breadcrumbs">{{ breadcrumb }}</small>
        <h1>{{ titulo }}</h1>
        <p>{{ descripcion }}</p>
      </div>
      <div class="acciones" *ngIf="accionTexto && accionRuta">
        <a class="btn-principal" [routerLink]="accionRuta">{{ accionTexto }}</a>
      </div>
    </header>
  `
})
export class PageHeaderComponent {
  @Input() breadcrumb = '';
  @Input() titulo = '';
  @Input() descripcion = '';
  @Input() accionTexto?: string;
  @Input() accionRuta?: string;
}
