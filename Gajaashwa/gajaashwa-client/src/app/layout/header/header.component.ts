import { Component } from '@angular/core';

@Component({
  selector: 'app-header',
  standalone: false,
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent {
  menuOpen = false;

  constructor() {
    window.addEventListener('click', (e) => {
      const target = e.target as HTMLElement;
      if (!target.closest('.hamburger') && !target.closest('nav')) {
        this.menuOpen = false;
      }
    });
  }
}
