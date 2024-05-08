import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Film } from 'src/app/Models/Film';
import { ServicePartenaireService } from 'src/app/Services/service-partenaire.service';

@Component({
  selector: 'app-detaill-film',
  templateUrl: './detaill-film.component.html',
  styleUrls: ['./detaill-film.component.css']
})
export class DetaillFilmComponent {
  id:number
  objectfilm: any

  constructor(private router:ActivatedRoute,private service:ServicePartenaireService){}
  ngOnInit():void{

    this.router.params.subscribe(param=>{
      this.id=param['id'];


    })
this.Getfilmparid()
  }


  Getfilmparid(){
    this.service.GetFilmparId(this.id).subscribe((res)=>{
    this.objectfilm=res
console.log("l'objet film",this.objectfilm)





    })
  }

}
