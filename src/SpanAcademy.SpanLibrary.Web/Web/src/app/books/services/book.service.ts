import { Inject, Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { BooksGridInfoDto } from '../models/booksGridInfoDto';
import { BookCodebookDto } from '../models/bookCodebookDto';

@Injectable({
  providedIn: 'root',
})
export class BookService {
  private serviceBaseUrl = '';

  constructor(
    private httpClient: HttpClient,
    @Inject('API_BASE_URL') public baseUrl: string
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
    const params = new HttpParams()
      .set('sortOrder', sortOrder)
      .set('searchValue', searchValue)
      .set('page', page)
      .set('pageSize', pageSize)
      .set('authorId', authorId)
      .set('publisherId', publisherId);

    return this.httpClient.get<BooksGridInfoDto>(`${this.serviceBaseUrl}`, {
      params: params,
    });
  }

  public getBookCodebooks(): Observable<BookCodebookDto> {
    return this.httpClient.get<BookCodebookDto>(
      `${this.serviceBaseUrl}/codebooks`
    );
  }
}
