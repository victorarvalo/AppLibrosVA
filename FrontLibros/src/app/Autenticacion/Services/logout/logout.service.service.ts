import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/app/enviroments/enviroment';

@Injectable({
  providedIn: 'root'
})
export class LogoutServiceService {

  private myAppUrl = environment.myAppUrl
  private myApiUrl = "api/AspNetUsers/GetLogOut"
  constructor(private http: HttpClient) { }

  GetLogOut():Observable<any>{
    return this.http.get(this.myAppUrl + this.myApiUrl, { observe: 'response'})
  }
}
