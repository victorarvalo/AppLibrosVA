import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-book',
  templateUrl: './book.component.html',
  styleUrls: ['./book.component.css']
})
export class BookComponent {
  @Input()
  bookLocal:any;

  constructor(private router: Router){

  }
showDetails() {
  this.router.navigateByUrl('bookdetails', {
    state: {book: this.bookLocal}
});
}

}
