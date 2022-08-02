import {
  AfterViewInit,
  Component,
  ElementRef,
  OnInit,
  ViewChild,
} from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSelect } from '@angular/material/select';
import { MatSort } from '@angular/material/sort';
import { debounceTime, forkJoin, fromEvent } from 'rxjs';
import { ConfirmDialogService } from '../shared/services/confirm-dialog.service';
import { NotificationService } from '../shared/services/notification.service';
import { BookDto } from './models/bookDto';
import { DropdownDto } from './models/dropdownDto';
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
  @ViewChild('author') authorSelect: MatSelect;
  @ViewChild('publisher') publisherSelect: MatSelect;

  public searchValue: string = '';
  private oldSearchValue: string = '';

  public displayedColumns = ['title', 'author', 'publisher', 'isbn', 'actions'];
  public selectedAuthorId: number;
  public selectedPublisherId: number;
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
    this.paginator.page.subscribe(() => this.getBooks());

    this.sort.sortChange.subscribe(() => this.resetPaginationAndGetBooks());

    fromEvent(this.searchInput.nativeElement, 'keyup')
      .pipe(debounceTime(500))
      .subscribe(() => {
        this.searchValue = this.searchValue?.trim();

        if (this.searchValue != this.oldSearchValue) {
          this.resetPaginationAndGetBooks();
        }

        this.oldSearchValue = this.searchValue;
      });

    this.authorSelect.registerOnChange((newAuthorId: number) => {
      this.selectedAuthorId = newAuthorId;
      this.resetPaginationAndGetBooks();
    });

    this.publisherSelect.registerOnChange((newPublisherId: number) => {
      this.selectedPublisherId = newPublisherId;
      this.resetPaginationAndGetBooks();
    });

    this.getBooks();
  }

  private resetPaginationAndGetBooks() {
    this.paginator.pageIndex = 0;
    this.getBooks();
  }

  private getBooks() {
    this.dataSource.loadBooks(
      this.sort.direction,
      this.searchValue,
      this.paginator.pageIndex + 1,
      this.paginator.pageSize,
      this.selectedAuthorId,
      this.selectedPublisherId
    );
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
