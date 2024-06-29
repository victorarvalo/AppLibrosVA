import { Component } from '@angular/core';
import { RegisterServiceService } from '../Services/register/register.service.service';
import {Router} from '@angular/router'

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})

export class RegisterComponent {

  private aspNetUser = {
    id: "",
    userName: "",
    normalizedUserName: null,
    email: "",
    normalizedEmail: null,
    emailConfirmed: false,
    passwordHash: "",
    securityStamp: null,
    concurrencyStamp: null,
    phoneNumber: null,
    phoneNumberConfirmed: false,
    twoFactorEnabled: false,
  }

  private response: any;

  email:any = "";
  password:any = "";

  constructor(private _registerService: RegisterServiceService,
    private router: Router
  ){

  }

  register(){
    this.aspNetUser.userName = this.email;
    this.aspNetUser.email = this.email;
    this.aspNetUser.passwordHash = this.password;
    this._registerService.PostAspNetUser(this.aspNetUser).subscribe(
      data => {
        this.response = data;
        console.info(this.response);
        this.router.navigate(['login']);
      },
      error=>{
        console.error(error);
      }
    )
  }
}
