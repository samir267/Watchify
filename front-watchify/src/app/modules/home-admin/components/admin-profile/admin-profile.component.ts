import { Component } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { User } from 'src/app/Models/User';
import { AdminService } from 'src/app/Services/admin.service';
import { JwtDecoderService } from 'src/app/service/jwt-decoder.service';

@Component({
  selector: 'app-admin-profile',
  templateUrl: './admin-profile.component.html',
  styleUrls: ['./admin-profile.component.css']
})
export class AdminProfileComponent {

  constructor(private serv:AdminService,private cookieService:CookieService,private jwtdecode:JwtDecoderService){}
  userInfo:any
  ngOnInit(){
    const token = this.cookieService.get('token');
    const role = this.cookieService.get('role');
    let decode=this.jwtdecode.decodeToken1(token)
    console.log(decode)

    this.serv.getUserById(decode.id).subscribe({
      next:(rep)=>{
        this.userInfo=rep;
        console.log(this.userInfo)
      }
    })
  }
}
