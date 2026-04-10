import { CommonModule } from '@angular/common';
import { Component, OnInit, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';

import { GimnasioApiService, Plan } from '../../servicios/gimnasio-api.service';

@Component({
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './planes-page.component.html'
})
export class PlanesPageComponent implements OnInit {
  private readonly api = inject(GimnasioApiService);
  private readonly fb = inject(FormBuilder);

  planes: Plan[] = [];

  formulario = this.fb.nonNullable.group({
    nombre: ['', Validators.required],
    descripcion: [''],
    precio: [0, Validators.required],
    duracionDias: [30, Validators.required],
    permiteAcceso: [true]
  });

  ngOnInit(): void {
    this.cargarPlanes();
  }

  cargarPlanes(): void {
    this.api.listarPlanes().subscribe((data) => (this.planes = data));
  }

  crear(): void {
    if (this.formulario.invalid) return;
    this.api.crearPlan(this.formulario.getRawValue()).subscribe(() => {
      this.formulario.reset({ nombre: '', descripcion: '', precio: 0, duracionDias: 30, permiteAcceso: true });
      this.cargarPlanes();
    });
  }
}
