import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { LoginComponent } from './Autenticacion/login/login.component';
import { RegisterComponent } from './Autenticacion/register/register.component';
import { FormsModule } from '@angular/forms';
import { DetailCategoryComponent } from './Library/Components/Category/detail-category/detail-category.component'
import { BookDetailComponent } from './Library/Components/Book/book-detail/book-detail.component';
import { ListBooksComponent } from './Library/Components/Book/list-books/list-books.component';
import { BookComponent } from './Library/Components/Book/book/book.component';

@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    LoginComponent,
    RegisterComponent,
    DetailCategoryComponent,
    BookDetailComponent,
    ListBooksComponent,
    BookComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
