import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Film } from 'src/app/Models/Film';
import { ServicePartenaireService } from 'src/app/Services/service-partenaire.service';
declare interface RouteInfo {
  path: string;
  title: string;
  icon: string;
  class: string;
}
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit  {

  id:number;
  objfilm:any;
  logoFilePath: string;
  logoUrl: any;
  constructor(private servicefilm:ServicePartenaireService,private router:ActivatedRoute){}

  ngOnInit(): void {
    this.router.params.subscribe(param=>{
      this.id=param['id'];
    })
    console.log(this.id);

    this.servicefilm.GetFilmParIdUser(this.id).subscribe((res)=>{

      this.objfilm=res;
      console.log("films",this.objfilm)

    });



this.getImage("602cbb55-1132-463e-af28-d95c47ed1969_use.png")

  }


getImage(id:string){
  this.servicefilm.getLogoFile(id).subscribe(res=>{
    console.log("hhhh",res)
  },error=>{
    console.log('Erreur : ', error.url);
    return error.url;
  })
}


}
