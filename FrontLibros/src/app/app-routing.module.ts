import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NavComponent } from './nav/nav.component';
import { LoginComponent } from './Autenticacion/login/login.component';
import { RegisterComponent } from './Autenticacion/register/register.component';
import { ListBooksComponent } from './Autenticacion/list-books/list-books.component';
import { BookComponent } from './Autenticacion/book/book.component';
import { BookDetailComponent } from './Autenticacion/book-detail/book-detail.component';
import { CreateResenaComponent } from './Library/Components/Resena/create.resena/create.resena.component';
import { AppComponent } from './app.component';

const routes: Routes = [
  {path:'nav',component:NavComponent},
  {path:'login',component:LoginComponent},
  {path:'register',component:RegisterComponent},
  {path:'list-books',component:ListBooksComponent},
  {path:'book',component:BookComponent},
  {path: 'bookdetails', component:BookDetailComponent},
  {path: 'createresena', component: CreateResenaComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
