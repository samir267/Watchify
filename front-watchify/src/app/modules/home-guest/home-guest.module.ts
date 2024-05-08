import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { HomeGuestRoutingModule } from './home-guest-routing.module';
import { WelcomeComponent } from './components/welcome/welcome.component';

import { IonicModule } from '@ionic/angular';

@NgModule({
  declarations: [
    WelcomeComponent
  ],
  imports: [
    CommonModule,
    HomeGuestRoutingModule,
    IonicModule.forRoot() // or IonicModule depending on your Angular version

  ]
})
export class HomeGuestModule { }
