import { CommonModule } from '@angular/common';
import { Component, OnInit, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';

import { GimnasioApiService, Membresia, Plan, Socio } from '../../servicios/gimnasio-api.service';

@Component({
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './membresias-page.component.html'
})
export class MembresiasPageComponent implements OnInit {
  private readonly api = inject(GimnasioApiService);
  private readonly fb = inject(FormBuilder);

  socios: Socio[] = [];
  planes: Plan[] = [];
  membresias: Membresia[] = [];

  formulario = this.fb.nonNullable.group({
    socioId: ['', Validators.required],
    planId: ['', Validators.required],
    fechaInicio: ['', Validators.required],
    montoAdeudado: [0, Validators.required]
  });

  ngOnInit(): void {
    this.api.listarSocios().subscribe((data) => (this.socios = data));
    this.api.listarPlanes().subscribe((data) => (this.planes = data));
    this.cargarMembresias();
  }

  cargarMembresias(): void {
    this.api.listarMembresias().subscribe((data) => (this.membresias = data));
  }

  crear(): void {
    if (this.formulario.invalid) return;
    this.api.crearMembresia(this.formulario.getRawValue()).subscribe(() => {
      this.formulario.reset({ socioId: '', planId: '', fechaInicio: '', montoAdeudado: 0 });
      this.cargarMembresias();
    });
  }
}
