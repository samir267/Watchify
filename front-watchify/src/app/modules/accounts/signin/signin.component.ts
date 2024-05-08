import { FacebookLoginProvider, SocialAuthService } from '@abacritt/angularx-social-login';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Auth } from 'src/app/model/auth';
import { AuthService } from 'src/app/service/auth.service';
import { UserService } from 'src/app/service/user.service';
import { CookieService } from 'ngx-cookie-service';
import { JwtDecoderService } from 'src/app/service/jwt-decoder.service';
import { User } from 'src/app/Models/User';

@Component({
  selector: 'app-signin',
  templateUrl: './signin.component.html',
  styleUrls: ['./signin.component.css']
})
export class SigninComponent {
  title = 'angular-facebook';
  user:any;
  loggedIn:any;
  email:string;
  password:string;
token:any;
decodedToken:any;
form: FormGroup;
decoded!:any;
decodeduser:any;
constructor(private authService1:AuthService,
  private authService: SocialAuthService,
  private userservice:UserService,
  private toastr: ToastrService,
  private fb: FormBuilder,
private route:Router,
private cookieService: CookieService,
private decodeJwt:JwtDecoderService){

}

ngOnInit(): void {
  this.form = this.fb.group({
    username: ['', [Validators.required, Validators.email]],
    password: ['', [Validators.required, Validators.pattern(/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()_+}{"':;?/><,.\\[\]\\=-])[A-Za-z\d!@#$%^&*()_+}{"':;?/><,.\\[\]\\=-]{8,}$/)]],
    // Add more form controls as needed
  });
  this.authService.authState.subscribe((user) => {
    this.user = user;
    console.log(this.user);
    const auths={
      "username":this.user.email ,
      "socialId":this.user.id
    }
    this.authService1.loginS(auths).subscribe(res=>{
      console.log(res)
      this.cookieService.set('token', res.token);

      this.toastr.success("succesfuly logged In", 'Error');

      this.route.navigate(['/customer'])
    },
  error=>{
    this.toastr.error('user not registred', 'Error');

    console.log("user maandy",error)

  })
    this.loggedIn = (user != null);
  });
}


login(){
  if (this.form.valid){
    this.authService1.login(this.form.value).subscribe(res=>{
      this.token=res

      this.cookieService.set('token', this.token.token);
      this.cookieService.set('role', this.token.role);
      this.toastr.success("succesfuly logged In", 'Error');
    this.decoded=this.decodeJwt.decodeToken1(this.token.token)
    console.log(this.decoded)
    this.userservice.getUserById(this.decoded.id).subscribe(
      res=>{
        console.log(res)
        this.decodeduser=res
        if(this.decodeduser.etat==0){
          console.log('metyaked',this.decodeduser.isEmailConfirmed)
          if(this.decodeduser.isEmailConfirmed==true){
            if(this.token.role==0){
              this.route.navigate(['/admin'])
             }else if(this.token.role==1){
               this.route.navigate(['/customer'])

             }else{
               this.route.navigate(['/partner'])

             }
          }else{
            this.route.navigate(['accounts/validation',this.decodeduser.username])


          }
        }else{
          this.toastr.error('your account is blocked please contact Administration', 'Error');

        }

      }
    )


    },
    error=>{
      console.log('error',error);
      this.toastr.error(error.error, 'Error');

    }
  )
  }else{
    this.toastr.error("you must validate the form", 'Error');

  }

}

signInWithFB(): void {
  this.authService.signIn(FacebookLoginProvider.PROVIDER_ID);
}

}
