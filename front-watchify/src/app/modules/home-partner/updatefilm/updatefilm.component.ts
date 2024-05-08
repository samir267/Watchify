import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ServicePartenaireService } from 'src/app/Services/service-partenaire.service';

@Component({
  selector: 'app-updatefilm',
  templateUrl: './updatefilm.component.html',
  styleUrls: ['./updatefilm.component.css']
})
export class UpdatefilmComponent {
  id:number
  objectfilm: any
  isfree:any
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
      this.isfree=res.IsFree






      })
    }

    updatefilm(){}
}
