import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/app/enviroments/enviroment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class LoginServiceService {

  private myAppUrl = environment.myAppUrl
  private myApiUrl = "api/AspNetUsers/GetLogin"
  constructor(private http: HttpClient) { }

  GetLogin(userName:string, password:string):Observable<any>{
    let queryParams = new HttpParams();
    queryParams = queryParams.append('userName', userName);
    queryParams = queryParams.append('password', password);
    const response = 'response'
    return this.http.get(this.myAppUrl + this.myApiUrl, { observe: 'response', params: queryParams })
  }
}
