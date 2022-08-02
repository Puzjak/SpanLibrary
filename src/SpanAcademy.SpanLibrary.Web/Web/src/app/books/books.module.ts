import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSelectModule } from '@angular/material/select';
import { MatSortModule } from '@angular/material/sort';
import { MatTableModule } from '@angular/material/table';
import { BooksRoutingModule } from './books-routing.module';
import { BooksComponent } from './books.component';

import { CreateBookComponent } from './create-book/create-book.component';

@NgModule({
  declarations: [BooksComponent, CreateBookComponent],
  imports: [
    CommonModule,
    BooksRoutingModule,
    MatButtonModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatInputModule,
    MatIconModule,
    FormsModule,
    MatSelectModule,
    MatCardModule,
    ReactiveFormsModule,
  ],
})
export class BooksModule {}
