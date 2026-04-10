import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';

import { PageHeaderComponent } from '../../../../shared/ui/page-header/page-header.component';
import { StatusBadgeComponent } from '../../../../shared/ui/status-badge/status-badge.component';
import { GimnasioApiService, Socio } from '../../servicios/gimnasio-api.service';

@Component({
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, PageHeaderComponent, StatusBadgeComponent],
  templateUrl: './acceso-page.component.html'
})
export class AccesoPageComponent {
  private readonly api = inject(GimnasioApiService);
  private readonly fb = inject(FormBuilder);

  socios: Socio[] = [];
  resultado = '';
  motivo = '';
  error = '';

  formulario = this.fb.nonNullable.group({
    criterio: ['', Validators.required]
  });

  buscarYValidar(): void {
    if (this.formulario.invalid) return;
    const criterio = this.formulario.getRawValue().criterio.trim().toLowerCase();

    this.api.listarSocios().subscribe({
      next: data => {
        this.socios = data;
        const socio = this.socios.find(s =>
          s.numeroSocio.toLowerCase() === criterio || s.documento.toLowerCase() === criterio
        );

        if (!socio) {
          this.error = 'No se encontró socio con ese número o documento.';
          this.resultado = '';
          return;
        }

        this.error = '';
        this.api.validarAcceso(socio.id).subscribe(r => {
          this.resultado = r.resultado;
          this.motivo = r.motivo;
        });
      },
      error: () => {
        this.error = 'No se pudo consultar socios para validar acceso.';
      }
    });
  }
}
