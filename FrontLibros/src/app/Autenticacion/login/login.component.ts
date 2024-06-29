import { Component, OnInit } from '@angular/core';
import { LoginServiceService } from '../Services/login/login.service.service';
import { Router } from '@angular/router';
import { AspNetUserData } from './../../AspNetUserData.Service'

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit{
  visible = true;
  email:any = "";
  password:any = "";
  constructor(private _loginService: LoginServiceService,
    private router: Router,
    private aspNetUserData: AspNetUserData){
  }

  ngOnInit(){
    this.visible = true;
  }

  login(){
    this._loginService.GetLogin(this.email, this.password).subscribe(
      data =>{
        if(data.status == 200){
          console.log('login ok');
          this.aspNetUserData.updateAspNetUser(this.email);
          this.router.navigate(['register']);
        }
      },
      error=>{
        console.error(error);
        this.visible = false;
      }
    )
  }
}
