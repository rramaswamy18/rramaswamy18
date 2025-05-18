import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-info-card',
  standalone: false,
  templateUrl: './info-card.component.html',
  styleUrl: './info-card.component.scss'
})
export class InfoCardComponent {
  @Input() image!: string;
  @Input() title!: string;
  @Input() description!: string;
  @Input() points!: string[];
  @Input() redirectTo!: string;
}
