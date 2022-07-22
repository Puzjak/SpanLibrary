import { Injectable } from '@angular/core';
import { BookDto } from '../models/bookDto';
import { CollectionViewer, DataSource } from '@angular/cdk/collections';
import { BehaviorSubject, Observable } from 'rxjs';
import { BookService } from './book.service';
import { BooksGridInfoDto } from '../models/booksGridInfoDto';

@Injectable({
  providedIn: 'root',
})
export class BookDataSourceService implements DataSource<BookDto> {
  private bookSubject = new BehaviorSubject<BookDto[]>([]);
  private totalCountSubject = new BehaviorSubject<number>(0);
  private noDataSubject = new BehaviorSubject<boolean>(true);

  public data$ = this.bookSubject.asObservable();
  public totalCount$ = this.totalCountSubject.asObservable();
  public noData$ = this.noDataSubject.asObservable();

  constructor(private bookService: BookService) {}

  connect(collectionViewer: CollectionViewer): Observable<readonly BookDto[]> {
    return this.data$;
  }

  disconnect(collectionViewer: CollectionViewer): void {
    this.bookSubject.complete();
    this.totalCountSubject.complete();
    this.noDataSubject.complete();
  }

  public loadBooks(
    sortOrder: string = '',
    searchValue: string = '',
    page: number = 1,
    pageSize: number = 10,
    authorId: number = -1,
    publisherId: number = -1
  ): void {
    this.bookService
      .getBooks(sortOrder, searchValue, page, pageSize, authorId, publisherId)
      .subscribe({
        next: (gridInfo: BooksGridInfoDto) => {
          this.bookSubject.next(gridInfo.books);
          this.totalCountSubject.next(gridInfo.totalCount);
          this.noDataSubject.next(gridInfo.books.length == 0);
        },
        error: () => {
          this.bookSubject.next([]);
          this.totalCountSubject.next(0);
          this.noDataSubject.next(true);
        },
      });
  }
}
