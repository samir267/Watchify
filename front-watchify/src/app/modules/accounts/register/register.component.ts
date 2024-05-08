import { SocialAuthService ,FacebookLoginProvider} from '@abacritt/angularx-social-login';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from 'src/app/service/auth.service';
import { UserService } from 'src/app/service/user.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {

user:any
  form: FormGroup;
  constructor(  private fb: FormBuilder,private authService1: AuthService
    ,private router:Router,
    private toastr: ToastrService,
    private userService:UserService,
    private authService: SocialAuthService,

){}
  ngOnInit(): void {
    this.form = this.fb.group({
      username: ['', [Validators.required, Validators.email]],
      name: ['',[Validators.required]],
      phoneNumber: ['',[Validators.required]],
      password: ['', [Validators.required, Validators.pattern(/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()_+}{"':;?/><,.\\[\]\\=-])[A-Za-z\d!@#$%^&*()_+}{"':;?/><,.\\[\]\\=-]{8,}$/)]],
      role: [1],
      etat: [0],

      // Add more form controls as needed
    });
    this.social()
}

/**
 * This function is used to register a new user
 * @param None
 * @return None
 */
register() {
  // console log the form value
  console.log(this.form.value);
  // Check if the form is valid
  if (this.form.valid) {
    // Subscribe to the register function from the AuthService
    this.authService1.register(this.form.value).subscribe(res => {
      // Console log the response
      console.log(res, "res");
      // Navigate to the validation page
      this.router.navigate(['accounts/validation', res.username]);
      // Create an object with the recipient email
      const email = {
        "recipientEmail": res.username
      }
      // Subscribe to the sendVerifEmail function from the UserService
      this.userService.sendVerifEmail(email).subscribe(res => {
        // Console log the response
        console.log("email sent", res);
      })
      // Show a success message to the user
      this.toastr.success('user  registred successfully', 'success');
    }, error => {
      // Console log the error
      console.log(error, "error");
      // Show an error message to the user
      this.toastr.error(error.error, 'Error');
    })
  }
}
signUpWithFB(): void {
  this.authService.signIn(FacebookLoginProvider.PROVIDER_ID);
}
social(){

  this.authService.authState.subscribe((user) => {
    this.user = user;
    console.log(this.user);
    const auths={

        "username": this.user.email,
        "role": 1,
        "name": this.user.firstName+" "+this.user.lastName,
        "phoneNumber": "null",
        "facebookId": this.user.id,
        "googleId": "null",
        "provider": this.user.provider,
        "etat": 0

    }


    this.authService1.registerS(auths).subscribe(res=>{
      this.toastr.success("succesfuly logged In", 'Error');
       console.log(res)
      this.router.navigate(['/'])
    },
  error=>{
    if(error.status==400){
      this.toastr.error(error.error, 'Error');
      this.router.navigate(['/accounts'])

    }else{
      this.toastr.error('error please try again', 'Error');

    }
console.log(error)

  })
  });
}
}

