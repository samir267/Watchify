import { Component } from '@angular/core';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { UserService } from 'src/app/service/user.service';

@Component({
  selector: 'app-email-validation',
  templateUrl: './email-validation.component.html',
  styleUrls: ['./email-validation.component.css']
})
export class EmailValidationComponent {

code: any;
email:any;
constructor(private userService:UserService,
  private toastr: ToastrService,
  private ActiveRoute:ActivatedRoute,
  private router:Router
){}
send() {
  const email = {
    "recipientEmail": this.email
  }
  // Subscribe to the sendVerifEmail function from the UserService
  this.userService.sendVerifEmail(email).subscribe(res => {
    // Console log the response
    this.toastr.success('Code Sended', 'Succes');
  },error=>{
    if(error.status==200){
      this.toastr.success('Code Sended', 'Succes');

    }
  })  }
  ngOnInit(): void {
  this.email=this.ActiveRoute.snapshot.params['email']
  }
confirm() {
  this.userService.VerifEmail(this.code,this.email).subscribe(res=>{
    console.log(res.status)
  },error=>{
    if(error.status==200){
      this.router.navigate(['/customer'])
      console.log('hhh')
    }else{
      this.toastr.error('Invalid Code', 'Error');

    }
  })
  }
}
