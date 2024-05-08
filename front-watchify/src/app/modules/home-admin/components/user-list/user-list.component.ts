import { Component } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { User } from 'src/app/Models/User';
import { AdminService } from 'src/app/Services/admin.service';



@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css'],
  
})
export class UserListComponent {
  allUsers:User[]=[];



  constructor(
    private adminserv:AdminService ,
    // private modalService: MdbModalService
    private Toast:ToastrService
       ){}
       

       Onsuccess(){
        this.Toast.success('User State Switched');
      }
      Onsuccess2(){
        this.Toast.success('User Deleted');
      }
      

      //   openDialog() {
      //   this.modalRef = this.modalService.open(EditUserComponent, {
      //     // modalOptions: {
      //     //   // Add any modal options here, such as size, backdrop, etc.
      //     //   size: 'lg' // Example: Large size
      //     // }
      //   });
      
      //   // Subscribe to the modal close event
      //   this.modalRef?.onClose.subscribe((result) => {
      //     // Handle modal close event here if needed
      //     console.log('Modal closed with result:', result);
      //   });
      //  }
      
  

  ngOnInit(){
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
    if(confirm("Are you sure to change user state ?")) {

    this.adminserv.deactivateUser(userId).subscribe(
      () => {
        this.Onsuccess();
        console.log("Utilisateur désactivé avec succès");
        this.getAll();
      },
      (error:any) => {
        console.error("Erreur lors de la désactivation de l'utilisateur :", error);
      }
    );
  }
  }

  deleteUser(userId: number){
    if(confirm("Are you sure to delete ?")) {

    this.adminserv.deleteUser(userId).subscribe(
      (res:any) => {
      
        console.log("Réponse du serveur :", res);
        
        if (res) {
          this.Onsuccess2();
          console.log("Réponse du serveur reçue :", res);

            console.log("Utilisateur supprimé avec succès");
            this.getAll();
          } else {
            console.error("Erreur lors de la suppression de l'utilisateur :", res);
            // Affichez un message d'erreur approprié à l'utilisateur ici
          }
        
      },
      (error:any) => {
        console.error("Erreur lors de la suppression de l'utilisateur :", error);
        // Affichez un message d'erreur approprié à l'utilisateur ici
      }
    );
  }
  }
  

  
}
