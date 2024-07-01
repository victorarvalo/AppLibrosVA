import { Injectable } from '@angular/core';
import { environment } from 'src/app/enviroments/enviroment';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { BookModel } from '../Models/BookModels';

@Injectable({
  providedIn: 'root'
})
export class BookServiceService {

  private myAppUrl = environment.myAppUrl
  private myApiUrl = "api/Book/"

  constructor(private http: HttpClient) { }

  getAllBooks():Observable<any>{
    return this.http.get(`${this.myAppUrl}${this.myApiUrl}GetBooks`)
  }

  getBookById(id:number):Observable<any>{
    return this.http.get(`${this.myAppUrl}${this.myApiUrl}GetBookById/${id}`, { observe: 'response'})
  }

  postCreateBook(Book:any):Observable<any>{
    return this.http.post(`${this.myAppUrl}${this.myApiUrl}PostBook`, Book);
  }

  putUpdateBook(id:number,Book:any):Observable<any>{
    return this.http.put(`${this.myAppUrl}${this.myApiUrl}PutBook/${id}`,Book);
  }
}
