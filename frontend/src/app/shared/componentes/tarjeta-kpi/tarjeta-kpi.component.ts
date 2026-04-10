import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-tarjeta-kpi',
  standalone: true,
  template: `
    <article class="kpi">
      <h3>{{ titulo }}</h3>
      <strong>{{ valor }}</strong>
      <p>{{ descripcion }}</p>
    </article>
  `,
  styles: [
    `.kpi{background:rgba(255,255,255,.04);border:1px solid rgba(255,255,255,.08);border-radius:12px;padding:12px}`,
    `h3{margin:0;color:#9db0d9;font-size:.9rem}`,
    `strong{font-size:1.4rem}`,
    `p{margin:4px 0 0;color:#9db0d9}`
  ]
})
export class TarjetaKpiComponent {
  @Input({ required: true }) titulo = '';
  @Input({ required: true }) valor = '';
  @Input() descripcion = '';
}
