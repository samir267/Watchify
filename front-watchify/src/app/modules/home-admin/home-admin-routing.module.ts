import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { UserListComponent } from './components/user-list/user-list.component';
import { AdminProfileComponent } from './components/admin-profile/admin-profile.component';
import { FilmListComponent } from './components/film-list/film-list.component';

const routes: Routes = [
  { 
    path: '', 
    component: HomeComponent,
    children: [
      {path: 'userList', component: UserListComponent },
      {path: 'adminProfile',component:AdminProfileComponent},
      {path: 'filmList',component:FilmListComponent}
      // Ajoutez d'autres routes enfants au besoin
    ]
  }
   
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class HomeAdminRoutingModule { }
