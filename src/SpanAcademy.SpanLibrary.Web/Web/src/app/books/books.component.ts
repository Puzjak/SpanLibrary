import {
  AfterViewInit,
  Component,
  ElementRef,
  OnInit,
  ViewChild,
} from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import {
  debounceTime,
  distinctUntilChanged,
  forkJoin,
  fromEvent,
  map,
} from 'rxjs';
import { ConfirmDialogService } from '../shared/services/confirm-dialog.service';
import { NotificationService } from '../shared/services/notification.service';
import { BookDto } from './models/bookDto';
import { DropdownDto } from './models/dropdownDto';
import { GetBooksDto } from './models/getBooksDto';
import { AuthorService } from './services/author.service';
import { BookDataSourceService } from './services/book-data-source.service';
import { BookService } from './services/book.service';
import { PublisherService } from './services/publisher.service';

@Component({
  selector: 'app-books',
  templateUrl: './books.component.html',
  styleUrls: ['./books.component.scss'],
})
export class BooksComponent implements OnInit, AfterViewInit {
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  @ViewChild('search') searchInput: ElementRef<HTMLInputElement>;

  public searchValue: string = '';

  public displayedColumns = ['title', 'author', 'publisher', 'isbn', 'actions'];
  public selectedAuthorId?: number;
  public selectedPublisherId?: number;
  public authors: DropdownDto[];
  public publishers: DropdownDto[];

  constructor(
    private bookService: BookService,
    private authorService: AuthorService,
    private publisherService: PublisherService,
    public dataSource: BookDataSourceService,
    private confirmDialogService: ConfirmDialogService,
    private notificationService: NotificationService
  ) {}

  ngOnInit(): void {
    this.getDropdownData();
  }

  ngAfterViewInit(): void {
    fromEvent(this.searchInput.nativeElement, 'keyup')
      .pipe(
        map((event: any) => event.target.value),
        debounceTime(500),
        distinctUntilChanged()
      )
      .subscribe((value) => {
        this.searchValue = value?.trim();

        this.resetPaginationAndGetBooks();
      });

    this.getBooks();
  }

  public resetPaginationAndGetBooks() {
    this.paginator.pageIndex = 0;
    this.getBooks();
  }

  public getBooks() {
    const model: GetBooksDto = {
      sortOrder: this.sort.direction,
      searchValue: this.searchValue,
      page: this.paginator.pageIndex + 1,
      pageSize: this.paginator.pageSize,
      authorId: this.selectedAuthorId,
      publisherId: this.selectedPublisherId,
    };
    this.dataSource.loadBooks(model);
  }

  private getDropdownData() {
    forkJoin([
      this.authorService.getAuthors(),
      this.publisherService.getPublishers(),
    ]).subscribe(([authors, publishers]) => {
      this.authors = authors;
      this.publishers = publishers;
    });
  }

  onDeleteClick(book: BookDto) {
    this.confirmDialogService.open(
      'Deleting a book',
      'Are you sure that you want to delete this book?',
      () => {
        this.bookService.deleteBook(book.id).subscribe((_) => {
          this.notificationService.show('Book successfully deleted');
          this.resetPaginationAndGetBooks();
        });
      }
    );
  }
}
