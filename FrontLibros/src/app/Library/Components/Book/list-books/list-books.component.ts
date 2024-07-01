import { Component, OnInit } from '@angular/core';
import { BookServiceService } from 'src/app/Library/Services/book.service.service';


@Component({
  selector: 'app-list-books',
  templateUrl: './list-books.component.html',
  styleUrls: ['./list-books.component.css']
})
export class ListBooksComponent implements OnInit {

  booksList:any[] = [];

  constructor(private _bookService: BookServiceService){

  }

  ngOnInit(): void {
    (this._bookService.getAllBooks()).subscribe(
      data=>{
        this.booksList = data;
      },error=>{
        console.error(error);
      });
  }
}
