import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { DropdownDto } from '../models/dropdownDto';

@Injectable({ providedIn: 'root' })
export class AuthorService {
  constructor(
    private httpClient: HttpClient,
    @Inject('API_BASE_URL') private baseUrl: string
  ) {}

  getAuthors(): Observable<DropdownDto[]> {
    return this.httpClient.get<DropdownDto[]>(`${this.baseUrl}/author`);
  }
}
