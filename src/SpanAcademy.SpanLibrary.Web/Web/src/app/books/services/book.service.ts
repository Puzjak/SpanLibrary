import { HttpClient, HttpParams } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BooksGridInfoDto } from '../models/booksGridInfoDto';
import { CreateBookModel } from '../models/create-book.model';

@Injectable({
  providedIn: 'root',
})
export class BookService {
  private serviceBaseUrl;

  constructor(
    private httpClient: HttpClient,
    @Inject('API_BASE_URL') private baseUrl: string
  ) {
    this.serviceBaseUrl = `${this.baseUrl}/Book`;
  }

  public getBooks(
    sortOrder: string,
    searchValue: string,
    page: number,
    pageSize: number,
    authorId: number,
    publisherId: number
  ): Observable<BooksGridInfoDto> {
    let params = new HttpParams()
      .set('sortOrder', sortOrder)
      .set('searchValue', searchValue)
      .set('page', page)
      .set('pageSize', pageSize);
    if (authorId) {
      params = params.set('authorId', authorId);
    }
    if (publisherId) {
      params = params.set('publisherId', publisherId);
    }

    return this.httpClient.get<BooksGridInfoDto>(`${this.serviceBaseUrl}`, {
      params: params,
    });
  }

  createBook(book: CreateBookModel) {
    return this.httpClient.post<void>(`${this.serviceBaseUrl}`, book);
  }
}
