import { Injectable } from '@angular/core';
import { environment } from 'src/app/enviroments/enviroment';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ResenaServiceService {

  private myAppUrl = environment.myAppUrl
  private myApiUrl = "api/Resenas/"

  constructor(private http: HttpClient) { }

  getAllResenas():Observable<any>{
    return this.http.get(`${this.myAppUrl}${this.myApiUrl}GetResenas`)
  }

  getResenasById(id:number):Observable<any>{
    return this.http.get(`${this.myAppUrl}${this.myApiUrl}GetResenaById/${id}`)
  }

  getResenasByBookId(id:number):Observable<any>{
    return this.http.get(`${this.myAppUrl}${this.myApiUrl}GetResenaByBookId/${id}`)
  }

  postCreateResenas(Resena:any):Observable<any>{
    return this.http.post(`${this.myAppUrl}${this.myApiUrl}PostResena`, Resena);
  }

  putUpdateResenas(id:number,Resena:any):Observable<any>{
    return this.http.put(`${this.myAppUrl}${this.myApiUrl}PutResena/${id}`,Resena);
  }
}
