import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  { path: '', loadChildren: () => import('./modules/home-guest/home-guest.module').then(m => m.HomeGuestModule) },
  { path: 'customer', loadChildren: () => import('./modules/home-customer/home-customer.module').then(m => m.HomeCustomerModule) },
  { path: 'admin', loadChildren: () => import('./modules/home-admin/home-admin.module').then(m => m.HomeAdminModule) },
  { path: 'partner', loadChildren: () => import('./modules/home-partner/home-partner.module').then(m => m.HomePartnerModule) },
  { path: 'accounts', loadChildren: () => import('./modules/accounts/accounts.module').then(m => m.AccountsModule) },

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
