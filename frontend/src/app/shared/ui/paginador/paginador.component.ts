import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-paginador',
  standalone: true,
  template: `
    <footer class="paginador" *ngIf="totalPaginas > 1">
      <button (click)="cambiar(paginaActual - 1)" [disabled]="paginaActual === 1">Anterior</button>
      <span>Página {{ paginaActual }} de {{ totalPaginas }}</span>
      <button (click)="cambiar(paginaActual + 1)" [disabled]="paginaActual === totalPaginas">Siguiente</button>
    </footer>
  `
})
export class PaginadorComponent {
  @Input() paginaActual = 1;
  @Input() totalPaginas = 1;
  @Output() paginaChange = new EventEmitter<number>();

  cambiar(pagina: number): void {
    if (pagina < 1 || pagina > this.totalPaginas) return;
    this.paginaChange.emit(pagina);
  }
}
