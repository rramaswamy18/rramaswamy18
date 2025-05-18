import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { HomepageRoutingModule } from './homepage-routing.module';
import { HomeComponent } from './home/home.component';
import { CarouselModule } from 'ngx-owl-carousel-o';
import { MatIconModule } from '@angular/material/icon';
import { InfoCardComponent } from './info-card/info-card.component';

@NgModule({
  declarations: [
    HomeComponent,
    InfoCardComponent
  ],
  imports: [
    CommonModule,
    HomepageRoutingModule,
    CarouselModule,
    MatIconModule
  ]
})
export class HomepageModule { }
