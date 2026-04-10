import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-filtros-barra',
  standalone: true,
  template: `
    <section class="filtros-barra">
      <input [placeholder]="placeholder" [value]="valor" (input)="emitir($event)">
      <ng-content></ng-content>
    </section>
  `
})
export class FiltrosBarraComponent {
  @Input() placeholder = 'Buscar...';
  @Input() valor = '';
  @Output() valorChange = new EventEmitter<string>();

  emitir(evento: Event): void {
    const valor = (evento.target as HTMLInputElement).value;
    this.valorChange.emit(valor);
  }
}
