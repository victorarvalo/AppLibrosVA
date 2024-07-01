import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NavComponent } from './nav/nav.component';
import { LoginComponent } from './Autenticacion/login/login.component';
import { RegisterComponent } from './Autenticacion/register/register.component';
import { ListBooksComponent } from './Library/Components/Book/list-books/list-books.component';
import { BookComponent } from './Library/Components/Book/book/book.component';
import { BookDetailComponent } from './Library/Components/Book/book-detail/book-detail.component';

const routes: Routes = [
  {path:'nav',component:NavComponent},
  {path:'login',component:LoginComponent},
  {path:'register',component:RegisterComponent},
  {path:'booklist',component:ListBooksComponent},
  {path:'book',component:BookComponent},
  {path: 'bookdetails', component:BookDetailComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
