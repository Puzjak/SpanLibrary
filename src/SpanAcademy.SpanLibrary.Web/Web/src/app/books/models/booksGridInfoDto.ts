import { BookDto } from './bookDto';

// ----------------------------------------------
//  Interface and model
// ----------------------------------------------
export interface IBooksGridInfoDto {
  totalCount: number;
  books: BookDto[];
}

export class BooksGridInfoDto implements IBooksGridInfoDto {
  public totalCount: number;
  public books: BookDto[];

  constructor(totalCount: number, books: BookDto[]) {
    this.totalCount = totalCount;
    this.books = books;
  }
}
