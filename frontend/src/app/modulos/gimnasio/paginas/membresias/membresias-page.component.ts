import { CommonModule } from '@angular/common';
import { Component, OnInit, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';

import { EmptyStateComponent } from '../../../../shared/ui/empty-state/empty-state.component';
import { LoadingStateComponent } from '../../../../shared/ui/loading-state/loading-state.component';
import { PageHeaderComponent } from '../../../../shared/ui/page-header/page-header.component';
import { StatusBadgeComponent } from '../../../../shared/ui/status-badge/status-badge.component';
import { GimnasioApiService, Membresia, Plan, Socio } from '../../servicios/gimnasio-api.service';

@Component({
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, PageHeaderComponent, StatusBadgeComponent, LoadingStateComponent, EmptyStateComponent],
  templateUrl: './membresias-page.component.html'
})
export class MembresiasPageComponent implements OnInit {
  private readonly api = inject(GimnasioApiService);
  private readonly fb = inject(FormBuilder);

  cargando = true;
  guardando = false;
  error = '';
  exito = '';

  socios: Socio[] = [];
  planes: Plan[] = [];
  membresias: Membresia[] = [];

  formulario = this.fb.nonNullable.group({
    socioId: ['', Validators.required],
    planId: ['', Validators.required],
    fechaInicio: ['', Validators.required],
    montoAdeudado: [0, Validators.min(0)]
  });

  ngOnInit(): void {
    this.recargar();
  }

  recargar(): void {
    this.cargando = true;
    this.api.listarSocios().subscribe(s => (this.socios = s));
    this.api.listarPlanes().subscribe(p => (this.planes = p));
    this.api.listarMembresias().subscribe({
      next: data => {
        this.membresias = data;
        this.cargando = false;
      },
      error: () => {
        this.error = 'No se pudieron cargar las membresías.';
        this.cargando = false;
      }
    });
  }

  socioNombre(id: string): string {
    const socio = this.socios.find(s => s.id === id);
    return socio ? `${socio.apellido}, ${socio.nombre}` : id;
  }

  planNombre(id: string): string {
    return this.planes.find(p => p.id === id)?.nombre ?? id;
  }

  guardar(): void {
    if (this.formulario.invalid) {
      this.formulario.markAllAsTouched();
      return;
    }

    this.guardando = true;
    this.error = '';
    this.exito = '';

    this.api.crearMembresia(this.formulario.getRawValue()).subscribe({
      next: () => {
        this.exito = 'Membresía generada correctamente.';
        this.guardando = false;
        this.formulario.reset({ socioId: '', planId: '', fechaInicio: '', montoAdeudado: 0 });
        this.recargar();
      },
      error: () => {
        this.error = 'No se pudo registrar la membresía.';
        this.guardando = false;
      }
    });
  }
}
