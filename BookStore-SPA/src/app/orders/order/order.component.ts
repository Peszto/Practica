import { Component, OnInit } from '@angular/core';
import { Order } from '../../_models/Order';
import { OrderService } from '../../_services/order.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { NgForm } from '@angular/forms';
import {
  debounceTime,
  distinctUntilChanged,
  map,
  switchMap,
} from 'rxjs/operators';
import { BookService } from '../../_services/book.service';
import { ClientService } from '../../_services/client.service';
import { Observable, of, OperatorFunction } from 'rxjs';
import { ApiResponse } from '../../_models/ApiResponse';
import { BasicModel } from '../../_models/BasicModel';
import { OrderWithClientAndBookName } from '../../_models/OrderWithClientAndBookName';

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrl: './order.component.css',
})
export class OrderComponent implements OnInit {
  public formData!: OrderWithClientAndBookName;
  public formDataTest!: OrderWithClientAndBookName;
  public orders: any;
  public books: any;
  public clients: any;
  public selectedBookPrice?: number | null;
  public isBookNameValid: boolean = false;
  public isClientNameValid: boolean = false;

  constructor(
    public service: OrderService,
    private bookService: BookService,
    private clientService: ClientService,
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
      this.loadOrderData(id);
    } else {
      this.resetForm();
    }
  }

  private loadOrderData(id: number) {
    this.service.getOrderById(id).subscribe(
      (order) => {
        this.formData = order;
        if (
          this.formData.bookId?.id != null &&
          this.formData.clientId?.id != null
        ) {
          this.isBookNameValid = true;
          this.isClientNameValid = true;
          this.selectedBookPrice =
            this.formData.totalPrice / this.formData.quantity!;
        }
      },
      (err) => {
        throw err;
      }
    );
  }

  public onSubmit(form: NgForm) {
    if (form.valid && this.isBookNameValid && this.isClientNameValid) {
      form.value.bookId = form.value.bookId.id;
      form.value.clientId = form.value.clientId.id;
      if (form.value.id === 0) {
        this.insertOrder(form);
      } else {
        this.updateOrder(form);
      }
    } else {
      this.toastr.error('Please ensure all fields are filled out correctly !');
    }
  }

  private insertOrder(form: NgForm) {
    this.service.addOrder(form.form.value).subscribe({
      next: (response: ApiResponse) => {
        if (response.success) {
          this.toastr.success(response.message);
          this.resetForm(form);
          this.router.navigate(['/orders']);
        } else {
          this.toastr.error(response.message);
        }
      },
      error: (err) => {
        throw err;
      },
    });
  }

  private updateOrder(form: NgForm) {
    this.service.updateOrder(this.formData.id, form.form.value).subscribe({
      next: (response: ApiResponse) => {
        if (response.success) {
          this.toastr.success(response.message);
          this.resetForm(form);
          this.router.navigate(['/orders']);
        } else {
          this.toastr.error(response.message);
        }
      },
      error: (err) => {
        throw err;
      },
    });
  }

  private resetForm(form?: NgForm) {
    if (form != null) {
      form.form.reset();
    }

    this.formData = {
      id: 0,
      clientId: null,
      bookId: null,
      quantity: null,
      orderNr: -1,
      totalPrice: 0,
    };
  }

  public cancel() {
    this.router.navigate(['/orders']);
  }

  searchBooks: OperatorFunction<string, readonly BasicModel[]> = (
    text$: Observable<string>
  ) =>
    text$.pipe(
      debounceTime(200),
      distinctUntilChanged(),
      switchMap((term) =>
        term.length >= 2
          ? this.bookService.filterBookNames(term).pipe(
              map((response: BasicModel[] | ApiResponse) => {
                if (Array.isArray(response)) {
                  this.isBookNameValid = true;
                  return response;
                } else {
                  this.toastr.error(response.message);
                  this.isBookNameValid = false;
                  return [];
                }
              })
            )
          : of([])
      )
    );

  searchClients: OperatorFunction<string, readonly BasicModel[]> = (
    text$: Observable<string>
  ) =>
    text$.pipe(
      debounceTime(200),
      distinctUntilChanged(),
      switchMap((term) =>
        term.length >= 2
          ? this.clientService.filterClientNames(term).pipe(
              map((response: BasicModel[] | ApiResponse) => {
                if (Array.isArray(response)) {
                  this.isClientNameValid = true;
                  return response;
                } else {
                  this.toastr.error(response.message);
                  this.isClientNameValid = false;
                  return [];
                }
              })
            )
          : of([])
      )
    );

  onBookSelect(event: any) {
    this.bookService.getBookById(event.item.id).subscribe((book) => {
      this.selectedBookPrice = book.price;
      this.formData.bookId = event.item;
      this.calculateTotalPrice();
    });
  }

  onClientSelect(event: any) {
    this.formData.clientId = event.item;
  }

  onQuantityChange() {
    this.calculateTotalPrice();
  }

  private calculateTotalPrice() {
    if (!this.formData.quantity || this.formData.quantity <= 0) {
      this.formData.totalPrice = 0;
    } else if (this.formData.quantity && this.selectedBookPrice) {
      this.formData.totalPrice =
        this.formData.quantity * this.selectedBookPrice;
    }
  }

  //formatter = (result: BasicModel) => result.Name;
  formatter = (x: { name: string }) => x.name;
}
