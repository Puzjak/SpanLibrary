export interface CreateBookModel {
  title: string;
  authorId: number;
  publisherId: number;
  isbn?: string | null;
  description?: string | null;
}
