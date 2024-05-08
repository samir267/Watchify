import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Film } from 'src/app/Models/Film';
import { ServicePartenaireService } from 'src/app/Services/service-partenaire.service';

@Component({
  selector: 'app-addfilm',
  templateUrl: './addfilm.component.html',
  styleUrls: ['./addfilm.component.css']
})
export class AddfilmComponent implements OnInit {

  constructor(private formBuilder: FormBuilder,private servicefilm:ServicePartenaireService){}
  AjouterFilm!: FormGroup;
  selectedFile: any;
  formadata:FormData=new FormData();
  videoFile: File | null = null;
  logoFile: File | null = null;



  ngOnInit(): void {

    this.AjouterFilm=this.formBuilder.group({
      id:["",Validators.required],
      titre:["",Validators.required],
      description:["",Validators.required],
      duree:["",Validators.required],
      genre:["",Validators.required],
      dateDeSortie:["",Validators.required],
      isFree:["",Validators.required],
      userId:["",Validators.required],
      videoFilePath:["",Validators.required],
      logoFilePath:["",Validators.required]
    })





  }

  onVideoFileSelected(event: any) {
    this.videoFile = event.target.files[0] as File;
  }

  onLogoFileSelected(event: any) {
    this.logoFile = event.target.files[0] as File;
  }


  ajoutrFilm()

  {


    this.formadata.append('Id',"1")
    this.formadata.append('Titre',this.AjouterFilm.value.titre)
    this.formadata.append('Description',this.AjouterFilm.value.description)
    this.formadata.append('Durée',this.AjouterFilm.value.duree)
    this.formadata.append('Genre',this.AjouterFilm.value.genre)
    this.formadata.append('DateDeSortie',this.AjouterFilm.value.dateDeSortie)
    this.formadata.append('IsFree',this.AjouterFilm.value.isFree)
    this.formadata.append('videoFile',this.videoFile)
    this.formadata.append('logoFile',this.logoFile)
    this.formadata.append('UserId',"2")



    this.servicefilm.AjouterFilm(this.formadata).subscribe((res)=>{
      alert("ajout avec succes");

    }

  )





  }

}
