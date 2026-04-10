import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';

import { GimnasioApiService } from '../../servicios/gimnasio-api.service';

@Component({
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './acceso-page.component.html'
})
export class AccesoPageComponent {
  private readonly api = inject(GimnasioApiService);
  private readonly fb = inject(FormBuilder);

  resultado = '';
  motivo = '';

  formulario = this.fb.nonNullable.group({
    socioId: ['', Validators.required]
  });

  validar(): void {
    if (this.formulario.invalid) return;
    this.api.validarAcceso(this.formulario.getRawValue().socioId).subscribe((r) => {
      this.resultado = r.resultado;
      this.motivo = r.motivo;
    });
  }
}
