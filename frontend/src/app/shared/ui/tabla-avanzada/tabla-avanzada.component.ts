import { CommonModule } from '@angular/common';
import { Component, Input, TemplateRef } from '@angular/core';

@Component({
  selector: 'app-tabla-avanzada',
  standalone: true,
  imports: [CommonModule],
  template: `
    <div class="tabla-wrap">
      <table class="tabla-avanzada" *ngIf="filas.length; else noData">
        <thead>
          <tr><th *ngFor="let c of columnas">{{ c }}</th></tr>
        </thead>
        <tbody>
          <tr *ngFor="let fila of filas">
            <ng-container *ngTemplateOutlet="filaTemplate; context: {$implicit: fila}"></ng-container>
          </tr>
        </tbody>
      </table>
      <ng-template #noData>
        <ng-content></ng-content>
      </ng-template>
    </div>
  `
})
export class TablaAvanzadaComponent<T> {
  @Input() columnas: string[] = [];
  @Input() filas: T[] = [];
  @Input({ required: true }) filaTemplate!: TemplateRef<unknown>;
}
