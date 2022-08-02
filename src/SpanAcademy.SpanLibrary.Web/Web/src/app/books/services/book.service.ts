import { HttpClient, HttpParams } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BooksGridInfoDto } from '../models/booksGridInfoDto';
import { CreateBookModel } from '../models/create-book.model';
import { GetBooksDto } from '../models/getBooksDto';

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

  public getBooks(getBooksDto: GetBooksDto): Observable<BooksGridInfoDto> {
    let params = new HttpParams();

    for (var property in getBooksDto) {
      if (
        getBooksDto.hasOwnProperty(property) &&
        getBooksDto[property as keyof GetBooksDto]
      ) {
        params = params.set(
          property,
          getBooksDto[property as keyof GetBooksDto] || ''
        );
      }
    }

    return this.httpClient.get<BooksGridInfoDto>(`${this.serviceBaseUrl}`, {
      params: params,
    });
  }

  createBook(book: CreateBookModel) {
    return this.httpClient.post<void>(`${this.serviceBaseUrl}`, book);
  }

  deleteBook(id: number) {
    return this.httpClient.delete<void>(`${this.serviceBaseUrl}/${id}`);
  }
}
