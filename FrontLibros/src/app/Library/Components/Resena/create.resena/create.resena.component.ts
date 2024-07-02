import { JsonPipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { ResenaServiceService } from 'src/app/Library/Services/resena.service.service';
import { SessionStorageService } from 'src/app/Storage/Sesion-Storage.Service';

@Component({
  selector: 'app-create.resena',
  templateUrl: './create.resena.component.html',
  styleUrls: ['./create.resena.component.css']
})
export class CreateResenaComponent implements OnInit{
  bookLocal:any;

  formulario: FormGroup;
  aspNetUserNav: string = '';

  constructor(private route: Router,
    private _sessionStorageService: SessionStorageService,
    private _resenaServiceService: ResenaServiceService
  ){
    this.bookLocal = (JSON.parse(
      JSON.stringify(this.route.getCurrentNavigation()?.extras.state)
    )).book;
    this.formulario = new FormGroup({
      calificacionresena: new FormControl(),
      descripcionresena: new FormControl()
    })
  }
  ngOnInit(): void {
    const lenghtSession = this._sessionStorageService.length;
    if(lenghtSession>0){
      const firstKey = this._sessionStorageService.key(0);
      const emailUser = this._sessionStorageService.getItem<string>(firstKey!);
      this.aspNetUserNav = emailUser !== null ? emailUser : '';
    }
    else{
      this.aspNetUserNav = '';
    }
  }

  createResena() {
    let resena = {
      "idlibro": 0,
      "calificacionresena": 0,
      "descripcionresena": '',
      "fecharesena": '',
      "aspnetuser": ''
    }
    const dataform = this.formulario.value;
    resena.idlibro = this.bookLocal.idlibro;
    resena.calificacionresena = Number(dataform.calificacionresena);
    resena.descripcionresena = dataform.descripcionresena;
    resena.aspnetuser = this.aspNetUserNav;
    resena.fecharesena = this.getDateFormat();
    console.log("reseña: " + JSON.stringify(resena))
    this._resenaServiceService.postCreateResenas(resena).subscribe(
      data=>{
              console.log("Reseña creada exitosamente!!");
      },error=>{
        console.log(error);
      }
    );
    this.route.navigateByUrl('bookdetails', {
      state: {book: this.bookLocal}
    });
  }

  getDateFormat(): string{
    const timeElapsed = Date.now();
    const today = new Date(timeElapsed)
    let mount,days;
    if(today.getMonth() < 10){
      mount = `0${today.getUTCMonth()}`;
    }else{
      mount = `${today.getUTCMonth()}`;
    }
    if(today.getDay() < 10){
      days = `0${today.getUTCDay()}`;
    }else{
      days = `${today.getUTCDate()}`;
    }
    return `${today.getUTCFullYear()}-${mount}-${days}`;
  }
}
