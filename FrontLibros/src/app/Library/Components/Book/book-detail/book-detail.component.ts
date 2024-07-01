import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ResenaServiceService } from 'src/app/Library/Services/resena.service.service';

@Component({
  selector: 'app-book-detail',
  templateUrl: './book-detail.component.html',
  styleUrls: ['./book-detail.component.css']
})
export class BookDetailComponent implements OnInit {


  @Input()
  bookLocal:any;

  resenalocal: any;

  constructor(private route: ActivatedRoute, private router: Router,
    private _resenaServiceService: ResenaServiceService
  ){
    this.bookLocal = (JSON.parse(
                        JSON.stringify(this.router.getCurrentNavigation()?.extras.state)
                      )).book;
   }

  ngOnInit(): void {
    this._resenaServiceService.getResenasByBookId(this.bookLocal.idlibro).subscribe(
      data=>{
        this.resenalocal = data;
        console.log(this.resenalocal)
      },error=>{
        console.log(error);
      }
    )
  }
}
