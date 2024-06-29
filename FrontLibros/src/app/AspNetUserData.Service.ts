import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class AspNetUserData{
  private aspNetUserSubject = new BehaviorSubject<string>('');
  aspNetUserValue$ = this.aspNetUserSubject.asObservable();

  async updateAspNetUser(newAspNetUser: string){
    await this.aspNetUserSubject.next(newAspNetUser);
    console.log('subject next');
    location.reload();
  }
}
