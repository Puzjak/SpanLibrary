import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BooksComponent } from './books.component';
import { CreateBookComponent } from './create-book/create-book.component';

const routes: Routes = [
  { path: '', component: BooksComponent },
  { path: 'create', component: CreateBookComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class BooksRoutingModule {}
