import { Component } from '@angular/core';
import { AdminService } from 'src/app/Services/admin.service';

@Component({
  selector: 'app-film-list',
  templateUrl: './film-list.component.html',
  styleUrls: ['./film-list.component.css']
})
export class FilmListComponent {
  constructor(private adminserv:AdminService){}
  allFilms:any[];

  ngOnInit(){
    this.getAll();
  }

  getAll(){
    return this.adminserv.getAllFilms().subscribe((res:any)=>{
      this.allFilms=res.value;
      console.log("films : ",this.allFilms)
     
    },(error:any)=>console.log("errrrrrrrr",error)
    )
  }

  getUserById(id:number){
    return this.adminserv.getUserById(id).subscribe({
      next:(res)=>{
        console.log(res.value.username);
      }
    })
  }
}
