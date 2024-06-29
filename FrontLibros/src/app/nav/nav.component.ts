import { Component, OnInit } from '@angular/core';
import { AspNetUserData } from '../AspNetUserData.Service';
import { LogoutServiceService } from '../Autenticacion/Services/logout/logout.service.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {


  aspNetUserNav: any
  private visible: any;
  ngOnInit(): void {
    this.aspNetUserNav = this.aspNetUserData.aspNetUserValue$;
    this.visible = true;
  }

  constructor(private aspNetUserData: AspNetUserData,
    private _logoutService: LogoutServiceService,
    private router: Router
  ){

  }

  logout() {
      this._logoutService.GetLogOut().subscribe(
        data=>{
          if(data.status == 200){
            console.log('logout ok');
            this.aspNetUserNav = '';
            this.router.navigate(['login']);
          }
      },
    error=>{
      console.log(error);
    })
    }
}
