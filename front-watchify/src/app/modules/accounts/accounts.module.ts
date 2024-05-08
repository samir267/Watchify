import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AccountsRoutingModule } from './accounts-routing.module';
import { SigninComponent } from './signin/signin.component';
import { FacebookLoginProvider, SocialAuthServiceConfig, SocialLoginModule } from '@abacritt/angularx-social-login';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { IonicModule } from '@ionic/angular';
import { RegisterComponent } from './register/register.component';
import { EmailValidationComponent } from './email-validation/email-validation.component';


@NgModule({
  declarations: [
    SigninComponent,
    RegisterComponent,
    EmailValidationComponent,

  ],
  imports: [
    CommonModule,
    AccountsRoutingModule,
    SocialLoginModule,
    FormsModule,
    IonicModule.forRoot() ,
    ReactiveFormsModule


  ],
  providers: [
    {
      provide: 'SocialAuthServiceConfig',
      useValue: {
        autoLogin: false,
        providers: [
          {
            id: FacebookLoginProvider.PROVIDER_ID,
            provider: new FacebookLoginProvider('1114819593109428')
          }
        ],
        onError: (err) => {
          console.error(err);
        }
      } as SocialAuthServiceConfig,
    }
  ],
})
export class AccountsModule { }
