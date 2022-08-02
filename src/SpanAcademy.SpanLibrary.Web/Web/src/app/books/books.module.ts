import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatTableModule } from '@angular/material/table';
import { SharedModule } from '../shared/shared.module';
import { BooksRoutingModule } from './books-routing.module';
import { BooksComponent } from './books.component';

import { CreateBookComponent } from './create-book/create-book.component';

@NgModule({
  declarations: [BooksComponent, CreateBookComponent],
  imports: [
    CommonModule,

    MatTableModule,
    MatPaginatorModule,
    MatSortModule,

    BooksRoutingModule,
    SharedModule,
  ],
})
export class BooksModule {}
