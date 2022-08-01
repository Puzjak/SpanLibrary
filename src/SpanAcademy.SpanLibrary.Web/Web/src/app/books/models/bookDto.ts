export class BookDto {
  public id: number;
  public title: string;
  public authorId: number;
  public authorName: string;
  public publisherId: number;
  public publisherName: string;
  public isbn: string;
  public isLoaned: boolean;

  constructor(
    id: number,
    title: string,
    authorId: number,
    authorName: string,
    publisherId: number,
    publisherName: string,
    isbn: string,
    isLoaned: boolean
  ) {
    this.id = id;
    this.title = title;
    this.authorId = authorId;
    this.authorName = authorName;
    this.publisherId = publisherId;
    this.publisherName = publisherName;
    this.isbn = isbn;
    this.isLoaned = isLoaned;
  }
}
