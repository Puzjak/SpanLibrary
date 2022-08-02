import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { forkJoin } from 'rxjs';
import { NotificationService } from 'src/app/shared/services/notification.service';
import { CreateBookModel } from '../models/create-book.model';
import { DropdownDto } from '../models/dropdownDto';
import { AuthorService } from '../services/author.service';
import { BookService } from '../services/book.service';
import { PublisherService } from '../services/publisher.service';

@Component({
  templateUrl: 'create-book.component.html',
})
export class CreateBookComponent implements OnInit {
  fg = this.fb.group({
    title: ['', [Validators.required, Validators.maxLength(1024)]],
    authorId: [null, [Validators.required]],
    publisherId: [null, [Validators.required]],
    yearPublished: [null, [Validators.required]],
    isbn: ['', [Validators.maxLength(20)]],
    description: [''],
  });
  public authors: DropdownDto[];
  public publishers: DropdownDto[];
  constructor(
    private authorService: AuthorService,
    private publisherService: PublisherService,
    private fb: FormBuilder,
    private bookService: BookService,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private notificationService: NotificationService
  ) {}

  ngOnInit() {
    forkJoin([
      this.authorService.getAuthors(),
      this.publisherService.getPublishers(),
    ]).subscribe(([authors, publishers]) => {
      this.authors = authors;
      this.publishers = publishers;
    });
  }

  getErrorMessage(ctrl: AbstractControl) {
    if (ctrl.errors?.['required']) {
      return 'This field is required';
    } else if (ctrl.errors?.['maxlength']) {
      return `Max length is ${ctrl.errors?.['maxlength'].requiredLength}`;
    }
    return '';
  }

  onCreateBookClick() {
    if (this.fg.invalid) {
      this.fg.markAllAsTouched();
      return;
    }

    const createModel: CreateBookModel = {
      title: this.fg.value.title!,
      authorId: this.fg.value.authorId!,
      publisherId: this.fg.value.publisherId!,
      description: this.fg.value.description,
      isbn: this.fg.value.isbn,
    };

    this.bookService.createBook(createModel).subscribe((_) => {
      this.notificationService.show('Book successfully created');
      this.router.navigate(['..'], { relativeTo: this.activatedRoute });
    });
  }
}
