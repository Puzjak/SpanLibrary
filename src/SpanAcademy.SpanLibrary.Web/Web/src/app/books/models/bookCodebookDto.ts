import { DropdownDto } from './dropdownDto';

// ----------------------------------------------
//  Interface and model
// ----------------------------------------------
export interface IBookCodebookDto {
  authors: DropdownDto[];
  publishers: DropdownDto[];
}

export class BookCodebookDto implements IBookCodebookDto {
  public authors: DropdownDto[];
  public publishers: DropdownDto[];

  constructor(authors: DropdownDto[], publishers: DropdownDto[]) {
    this.authors = authors;
    this.publishers = publishers;
  }
}
