import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/app/enviroments/enviroment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RegisterServiceService {

  private myAppUrl = environment.myAppUrl
  private myApiUrl = "api/AspNetUsers/PostAspNetUser"
  constructor(private http: HttpClient) { }

  PostAspNetUser(aspNetUser:any):Observable<any>{
    return this.http.post(this.myAppUrl + this.myApiUrl,aspNetUser)
  }
}
