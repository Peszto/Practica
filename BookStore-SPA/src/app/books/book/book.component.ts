import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgForm } from '@angular/forms';
import { Book } from '../../_models/Book';
import { BookService } from '../../_services/book.service';
import { ToastrService } from 'ngx-toastr';
import { CategoryService } from '../../_services/category.service';
import { ApiResponse } from '../../_models/ApiResponse';

@Component({
  selector: 'app-book',
  templateUrl: './book.component.html',
  styleUrls: ['./book.component.css'],
})
export class BookComponent implements OnInit {
  public formData!: Book;
  public categories: any;

  constructor(
    private categoryService: CategoryService,
    public service: BookService,
    private router: Router,
    private route: ActivatedRoute,
    private toastr: ToastrService
  ) {}

  ngOnInit() {
    this.resetForm();
    let id;
    this.route.params.subscribe((params) => {
      id = params['id'];
    });

    if (id != null) {
      this.loadBookData(id);
    } else {
      this.resetForm();
    }

    this.getCategories();
  }

  private getCategories(){
    this.categoryService.getCategories().subscribe(categories => {
      this.categories = categories;
    }, err => {
      this.toastr.error('An error occurred on get the records.');
    });

  }

  private loadBookData(id: number) {
    this.service.getBookById(id).subscribe(
      (book) => {
        this.formData = book;
        console.log(this.formData);
        const publishDate = new Date(book.publishDate);
        this.formData.publishDate = {
          year: publishDate.getFullYear(),
          month: publishDate.getMonth()+1,
          day: publishDate.getDate(),
        };
      },
      (err) => {
        throw err;
      }
    );
  }

  public onSubmit(form: NgForm) {
    form.value.publishDate = this.convertStringToDate(form.value.publishDate);
    form.value.categoryId = form.value.categoryId.id;

    if (form.value.id === 0) {
      this.insertRecord(form);
    } else {
      this.updateRecord(form);
    }
  }

  public insertRecord(form: NgForm) {
    this.service.addBook(form.form.value).subscribe({
      next: (response: ApiResponse) => {
        if (response.success) {
          this.toastr.success(response.message);
          this.resetForm(form);
          this.router.navigate(['/books']);
        } else {
          this.toastr.error(response.message);
        }
      },
      error: (err) => {
        throw err;
      },
    });
  }

  public updateRecord(form: NgForm) {
    this.service.updateBook(form.form.value.id, form.form.value).subscribe({
      next: (response: ApiResponse) => {
        if (response.success) {
          this.toastr.success(response.message);
          this.resetForm(form);
          this.router.navigate(['/books']);
        } else {
          this.toastr.error(response.message);
        }
      },
      error: (err) => {
        throw err;
      },
    });
  }

  public cancel() {
    this.router.navigate(['/books']);
  }

  private resetForm(form?: NgForm) {
    if (form != null) {
      form.form.reset();
    }

    this.formData = {
      id: 0,
      name: '',
      author: '',
      description: '',
      price: null,
      publishDate: null,
      categoryId: null,
      pieces: null,
    };
  }

  private convertStringToDate(date: any) {
    return new Date(`${date.year}-${date.month}-${date.day}`);
  }
}
