import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SigninComponent } from './signin/signin.component';
import { RegisterComponent } from './register/register.component';
import { EmailValidationComponent } from './email-validation/email-validation.component';

const routes: Routes = [
  { path: '', component: SigninComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'validation/:email', component: EmailValidationComponent },


];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AccountsRoutingModule { }
