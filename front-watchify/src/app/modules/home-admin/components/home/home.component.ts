import { CookieService } from 'ngx-cookie-service';
import { JwtDecoderService } from 'src/app/service/jwt-decoder.service';
import { Component, OnInit } from '@angular/core';
import { Chart } from 'chart.js';
import { User } from 'src/app/Models/User';
import { AdminService } from 'src/app/Services/admin.service';


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  constructor(private cookieService: CookieService,private jwtdecode:JwtDecoderService,private adminserv:AdminService) { }


  allUsers:User[]=[];


  ngOnInit(){
       // Your initialization logic here

    // Check if token and role are saved in cookies
    const token = this.cookieService.get('token');
    const role = this.cookieService.get('role');

    if (token && role) {
      console.log('Token and role are saved in cookies:', token, role);
      console.log(this.jwtdecode.decodeToken1(token))

    } else {
      console.log('Token and role are not saved in cookies');
    }
    console.log("la liste est ",this.getAll());
  }
  getAll(){
    return this.adminserv.getAllUsers().subscribe((res:any)=>{
      this.allUsers=res;
      console.log("aaaaaa",this.allUsers)
    },(error:any)=>console.log("errrrrrrrr",error)
    )
  }


  updateUser(userId: number, updatedUserData: any){
    this.adminserv.updateUser(userId, updatedUserData).subscribe(
      (res:any) => {
        console.log("Utilisateur mis à jour :", res);
      },
      (error:any) => {
        console.error("Erreur lors de la mise à jour de l'utilisateur :", error);
      }
    );
  }

  deactivateUser(userId: number){
    this.adminserv.deactivateUser(userId).subscribe(
      (res:any) => {
        console.log("Utilisateur désactivé avec succès");
      },
      (error:any) => {
        console.error("Erreur lors de la désactivation de l'utilisateur :", error);
      }
    );
  }
}
