import { CommonModule } from '@angular/common';
import { Component, OnInit, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';

import { PageHeaderComponent } from '../../../../shared/ui/page-header/page-header.component';
import { GimnasioApiService } from '../../servicios/gimnasio-api.service';

@Component({
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterLink, PageHeaderComponent],
  templateUrl: './planes-form.component.html'
})
export class PlanesFormComponent implements OnInit {
  private readonly api = inject(GimnasioApiService);
  private readonly fb = inject(FormBuilder);
  private readonly route = inject(ActivatedRoute);
  private readonly router = inject(Router);

  idEdicion: string | null = null;
  guardando = false;
  error = '';

  formulario = this.fb.nonNullable.group({
    nombre: ['', Validators.required],
    descripcion: [''],
    precio: [0, Validators.min(1)],
    duracionDias: [30, Validators.min(1)],
    permiteAcceso: [true]
  });

  ngOnInit(): void {
    this.idEdicion = this.route.snapshot.paramMap.get('id');
    if (!this.idEdicion) return;

    this.api.listarPlanes().subscribe(data => {
      const plan = data.find(p => p.id === this.idEdicion);
      if (plan) this.formulario.patchValue(plan);
    });
  }

  guardar(): void {
    if (this.formulario.invalid) {
      this.formulario.markAllAsTouched();
      return;
    }

    this.guardando = true;
    const request = this.idEdicion
      ? this.api.actualizarPlan(this.idEdicion, this.formulario.getRawValue())
      : this.api.crearPlan(this.formulario.getRawValue());

    request.subscribe({
      next: () => this.router.navigate(['/gimnasio/planes']),
      error: () => {
        this.error = 'No se pudo guardar el plan.';
        this.guardando = false;
      }
    });
  }
}
