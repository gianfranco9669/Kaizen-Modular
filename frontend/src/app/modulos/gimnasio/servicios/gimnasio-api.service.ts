import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { environment } from '../../../../environments/environment';

export interface Socio {
  id: string;
  numeroSocio: string;
  nombre: string;
  apellido: string;
  documento: string;
  correo: string;
  telefono: string;
  activo: boolean;
}

export interface Plan {
  id: string;
  nombre: string;
  descripcion: string;
  precio: number;
  duracionDias: number;
  permiteAcceso: boolean;
  activo: boolean;
}

export interface Membresia {
  id: string;
  socioId: string;
  planId: string;
  fechaInicio: string;
  fechaFin: string;
  estadoVigencia: string;
  estadoDeuda: string;
  montoTotal: number;
  montoAdeudado: number;
}

export interface ImpactoComercial {
  id: string;
  moduloOrigen: string;
  tipoOperacion: string;
  referenciaExterna: string;
  descripcion: string;
  monto: number;
  fechaOperacionUtc: string;
}

@Injectable({ providedIn: 'root' })
export class GimnasioApiService {
  private readonly baseUrl = environment.apiBaseUrl;

  constructor(private readonly http: HttpClient) {}

  listarSocios(): Observable<Socio[]> {
    return this.http.get<Socio[]>(`${this.baseUrl}/gimnasio/socios`);
  }

  crearSocio(payload: Omit<Socio, 'id' | 'activo'>): Observable<Socio> {
    return this.http.post<Socio>(`${this.baseUrl}/gimnasio/socios`, payload);
  }

  listarPlanes(): Observable<Plan[]> {
    return this.http.get<Plan[]>(`${this.baseUrl}/gimnasio/planes`);
  }

  crearPlan(payload: Omit<Plan, 'id' | 'activo'>): Observable<Plan> {
    return this.http.post<Plan>(`${this.baseUrl}/gimnasio/planes`, payload);
  }

  listarMembresias(): Observable<Membresia[]> {
    return this.http.get<Membresia[]>(`${this.baseUrl}/gimnasio/membresias`);
  }

  crearMembresia(payload: { socioId: string; planId: string; fechaInicio: string; montoAdeudado: number }): Observable<Membresia> {
    return this.http.post<Membresia>(`${this.baseUrl}/gimnasio/membresias`, payload);
  }

  validarAcceso(socioId: string): Observable<{ resultado: string; motivo: string }> {
    return this.http.post<{ resultado: string; motivo: string }>(`${this.baseUrl}/gimnasio/membresias/validar-acceso`, { socioId });
  }

  listarImpactosAdministracion(): Observable<ImpactoComercial[]> {
    return this.http.get<ImpactoComercial[]>(`${this.baseUrl}/administracion/impactos`);
  }
}
