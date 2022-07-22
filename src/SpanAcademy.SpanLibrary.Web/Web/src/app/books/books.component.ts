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
import { fromEvent, debounceTime } from 'rxjs';
import { BookCodebookDto } from './models/bookCodebookDto';
import { BookDataSourceService } from './services/book-data-source.service';
import { BookService } from './services/book.service';

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
  public dataSource: BookDataSourceService;
  public selectedAuthorId: number = -1;
  public selectedPublisherId: number = -1;
  public bookCodebook: BookCodebookDto;

  constructor(private bookService: BookService) {
    this.dataSource = new BookDataSourceService(this.bookService);
  }

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
    this.bookService
      .getBookCodebooks()
      .subscribe(
        (bookCodebook: BookCodebookDto) => (this.bookCodebook = bookCodebook)
      );
  }
}
