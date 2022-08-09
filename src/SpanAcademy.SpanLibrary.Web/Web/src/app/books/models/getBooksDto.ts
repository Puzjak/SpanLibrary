export interface GetBooksDto {
  sortOrder: string;
  searchValue: string;
  page: number;
  pageSize: number;
  authorId?: number;
  publisherId?: number;
}
