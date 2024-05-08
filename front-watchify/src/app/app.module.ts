import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ToastrModule } from 'ngx-toastr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ReactiveFormsModule } from '@angular/forms';
import { AdminService } from './Services/admin.service';
import { HttpClientModule } from '@angular/common/http';
import { UserListComponent } from './modules/home-admin/components/user-list/user-list.component';
import { AdminProfileComponent } from './modules/home-admin/components/admin-profile/admin-profile.component';
import { FilmListComponent } from './modules/home-admin/components/film-list/film-list.component';

@NgModule({
  declarations: [
    AppComponent,
    UserListComponent,
    AdminProfileComponent,
    FilmListComponent,

  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    ReactiveFormsModule,
    ToastrModule.forRoot() // ToastrModule added


  ],
  providers: [AdminService],
  bootstrap: [AppComponent]
})
export class AppModule { }
