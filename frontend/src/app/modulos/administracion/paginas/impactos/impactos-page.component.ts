import { CommonModule } from '@angular/common';
import { Component, OnInit, inject } from '@angular/core';

import { AdministracionApiService, ImpactoComercial } from '../../servicios/administracion-api.service';

@Component({
  standalone: true,
  imports: [CommonModule],
  templateUrl: './impactos-page.component.html'
})
export class ImpactosPageComponent implements OnInit {
  private readonly api = inject(AdministracionApiService);

  impactos: ImpactoComercial[] = [];

  ngOnInit(): void {
    this.api.listarImpactos().subscribe((data) => (this.impactos = data));
  }
}
