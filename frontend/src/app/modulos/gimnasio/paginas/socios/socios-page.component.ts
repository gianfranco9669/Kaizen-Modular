import { CommonModule } from '@angular/common';
import { Component, OnInit, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';

import { GimnasioApiService, Socio } from '../../servicios/gimnasio-api.service';

@Component({
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './socios-page.component.html'
})
export class SociosPageComponent implements OnInit {
  private readonly api = inject(GimnasioApiService);
  private readonly fb = inject(FormBuilder);

  socios: Socio[] = [];

  formulario = this.fb.nonNullable.group({
    numeroSocio: ['', Validators.required],
    nombre: ['', Validators.required],
    apellido: ['', Validators.required],
    documento: ['', Validators.required],
    correo: [''],
    telefono: ['']
  });

  ngOnInit(): void {
    this.cargarSocios();
  }

  cargarSocios(): void {
    this.api.listarSocios().subscribe((data) => (this.socios = data));
  }

  crear(): void {
    if (this.formulario.invalid) return;
    this.api.crearSocio(this.formulario.getRawValue()).subscribe(() => {
      this.formulario.reset();
      this.cargarSocios();
    });
  }
}
