import { Injectable } from '@angular/core';
import { environment } from 'src/app/enviroments/enviroment';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class CategoryServiceService {

  private myAppUrl = environment.myAppUrl
  private myApiUrl = "api/Category/"

  constructor(private http: HttpClient) { }

  getAllCategories():Observable<any>{
    return this.http.get(`${this.myAppUrl}${this.myApiUrl}GetCategories`)
  }

  getCategoryById(id:number):Observable<any>{
    return this.http.get(`${this.myAppUrl}${this.myApiUrl}GetCategoryById/${id}`, { observe: 'response'})
  }

  postCreateCategory(Category:any):Observable<any>{
    return this.http.post(`${this.myAppUrl}${this.myApiUrl}PostCategory`, Category);
  }

  putUpdateCategory(id:number,Category:any):Observable<any>{
    return this.http.put(`${this.myAppUrl}${this.myApiUrl}PutCategory/${id}`,Category);
  }

}
