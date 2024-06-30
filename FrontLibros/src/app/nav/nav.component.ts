import { Component, OnInit } from '@angular/core';
import { LogoutServiceService } from '../Autenticacion/Services/logout/logout.service.service';
import { Router } from '@angular/router';
import { SessionStorageService } from '../Storage/Sesion-Storage.Service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {


  aspNetUserNav: any;


  ngOnInit(): void {
    const lenghtSession = this._sessionStorageService.length;
    if(lenghtSession>0){
      const firstKey = this._sessionStorageService.key(0);
      const emailUser = this._sessionStorageService.getItem<string>(firstKey!);
      this.aspNetUserNav = emailUser;
    }
    else{
      this.aspNetUserNav = '';
    }
  }

  constructor(
    private _logoutService: LogoutServiceService,
    private router: Router,
    private _sessionStorageService: SessionStorageService
  ){

  }

  logout() {
      this._logoutService.GetLogOut().subscribe(
        data=>{
          if(data.status == 200){
            this._sessionStorageService.clear();
            this.router.navigate(['login']);
            window.location.reload();
          }
      },
    error=>{
      console.log(error);
    })
    }
}
