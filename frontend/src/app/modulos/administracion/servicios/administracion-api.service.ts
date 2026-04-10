import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { environment } from '../../../../environments/environment';

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
export class AdministracionApiService {
  private readonly baseUrl = environment.apiBaseUrl;

  constructor(private readonly http: HttpClient) {}

  listarImpactos(): Observable<ImpactoComercial[]> {
    return this.http.get<ImpactoComercial[]>(`${this.baseUrl}/administracion/impactos`);
  }
}
