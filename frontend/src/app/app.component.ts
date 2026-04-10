import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { NavigationEnd, Router, RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterOutlet, RouterLink, RouterLinkActive],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  breadcrumb = 'Inicio';

  constructor(private readonly router: Router) {
    this.router.events.subscribe(evento => {
      if (evento instanceof NavigationEnd) {
        this.breadcrumb = evento.urlAfterRedirects
          .replace(/^\//, '')
          .split('/')
          .filter(Boolean)
          .map(x => x.charAt(0).toUpperCase() + x.slice(1))
          .join(' / ') || 'Inicio';
      }
    });
  }
}
