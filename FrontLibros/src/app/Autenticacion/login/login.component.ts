import { Component, OnInit } from '@angular/core';
import { LoginServiceService } from '../Services/login/login.service.service';
import { Router } from '@angular/router';
import { SessionStorageService } from 'src/app/Storage/Sesion-Storage.Service';

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
    private _sessionStorageService: SessionStorageService){
  }

  ngOnInit(){
    this.visible = true;
  }

  login(){
    this._loginService.GetLogin(this.email, this.password).subscribe(
      data =>{
        if(data.status == 200){
          this._sessionStorageService.setItem(this.email,this.email);
          this.router.navigate(['nav'])
            .then(() => {
              window.location.reload();
            });
<<<<<<< HEAD
            this.router.navigateByUrl('booklist');
=======
          this.router.navigate(['list-books']);
>>>>>>> e5d3b116dd751bbff4134d913f661bcdc6e42323
        }
      },
      error=>{
        console.error(error);
        this.visible = false;
      }
    )
  }
}
