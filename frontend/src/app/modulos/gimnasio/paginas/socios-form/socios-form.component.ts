import { CommonModule } from '@angular/common';
import { Component, OnInit, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';

import { PageHeaderComponent } from '../../../../shared/ui/page-header/page-header.component';
import { GimnasioApiService } from '../../servicios/gimnasio-api.service';

@Component({
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterLink, PageHeaderComponent],
  templateUrl: './socios-form.component.html'
})
export class SociosFormComponent implements OnInit {
  private readonly api = inject(GimnasioApiService);
  private readonly fb = inject(FormBuilder);
  private readonly route = inject(ActivatedRoute);
  private readonly router = inject(Router);

  idEdicion: string | null = null;
  guardando = false;
  error = '';
  exito = '';

  formulario = this.fb.nonNullable.group({
    numeroSocio: ['', Validators.required],
    nombre: ['', Validators.required],
    apellido: ['', Validators.required],
    documento: ['', Validators.required],
    correo: [''],
    telefono: ['']
  });

  ngOnInit(): void {
    this.idEdicion = this.route.snapshot.paramMap.get('id');
    if (!this.idEdicion) return;

    this.api.listarSocios().subscribe(data => {
      const socio = data.find(s => s.id === this.idEdicion);
      if (!socio) return;
      this.formulario.patchValue(socio);
    });
  }

  guardar(): void {
    if (this.formulario.invalid) {
      this.formulario.markAllAsTouched();
      return;
    }

    this.guardando = true;
    this.error = '';
    this.exito = '';

    const request = this.idEdicion
      ? this.api.actualizarSocio(this.idEdicion, this.formulario.getRawValue())
      : this.api.crearSocio(this.formulario.getRawValue());

    request.subscribe({
      next: () => {
        this.exito = 'Socio guardado correctamente.';
        this.guardando = false;
        setTimeout(() => this.router.navigate(['/gimnasio/socios']), 700);
      },
      error: () => {
        this.error = 'No se pudo guardar el socio.';
        this.guardando = false;
      }
    });
  }
}
