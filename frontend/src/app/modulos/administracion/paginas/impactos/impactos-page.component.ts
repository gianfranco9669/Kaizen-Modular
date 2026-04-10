import { CommonModule } from '@angular/common';
import { Component, OnInit, inject } from '@angular/core';

import { GimnasioApiService, ImpactoComercial } from '../../../gimnasio/servicios/gimnasio-api.service';

@Component({
  standalone: true,
  imports: [CommonModule],
  templateUrl: './impactos-page.component.html'
})
export class ImpactosPageComponent implements OnInit {
  private readonly api = inject(GimnasioApiService);

  impactos: ImpactoComercial[] = [];

  ngOnInit(): void {
    this.api.listarImpactosAdministracion().subscribe((data) => (this.impactos = data));
  }
}
