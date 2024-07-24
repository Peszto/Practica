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
  filter,
} from 'rxjs/operators';
import { BookService } from '../../_services/book.service';
import { ClientService } from '../../_services/client.service';
import { Observable } from 'rxjs';
import { error } from 'console';
import { SuccessResponse } from '../../_models/SuccessResponse';
import { Server } from 'http';

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrl: './order.component.css',
})
export class OrderComponent implements OnInit {
  public formData!: Order;
  public orders: any;
  public books: any;
  public clients: any;

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
      this.service.getOrderById(id).subscribe(
        (book) => {
          this.formData = book;
        },
        (err) => {
          this.toastr.error('An error occurred on get the order.');
        }
      );
    } else {
      this.resetForm();
    }
  }

  public onSubmit(form: NgForm) {
    if (form.value.id === 0) {
      form.value.bookId = form.value.bookId.id;
      form.value.clientId = form.value.clientId.id;
      this.insertOrder(form);
    } else {
      this.updateOrder(form);
    }
  }

  private insertOrder(form: NgForm) {
    this.service.addOrder(form.form.value).subscribe({
      next: (response: SuccessResponse) => {
        if (response.success) {
          this.toastr.success(response.successMessage);
          this.resetForm(form);
          this.router.navigate(['/orders']);
        } else {
          this.toastr.error(response.successMessage);
        }
      },
      error: (err) => {

        throw err;
      },
    });
  }

  private updateOrder(form: NgForm) {
    this.service.updateOrder(form.form.value.id, form.form.value).subscribe({
      next: (response: SuccessResponse) => {
        if (response.success) {
          this.toastr.success(response.successMessage);
          this.resetForm(form);
          this.router.navigate(['/orders']);
        } else {
          this.toastr.error(response.successMessage);
        }
      },
      error: (err) => {
        this.toastr.error('An error occured on update of the order.');
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
      orderNr: null,
      totalPrice: 0,
    };
  }

  public cancel() {
    this.router.navigate(['/orders']);
  }

  searchBooks = (text$: Observable<string>) =>
    text$.pipe(
      debounceTime(300),
      distinctUntilChanged(),
      filter((term: string) => term.length >= 2),
      switchMap((term) => this.bookService.filterBookNames(term))
    );

  searchClients = (text$: Observable<string>) =>
    text$.pipe(
      debounceTime(300),
      distinctUntilChanged(),
      filter((term: string) => term.length >= 2),
      switchMap((term) => this.clientService.filterClientNames(term))
    );

  onBookSelect(event: any) {
    this.formData.bookId = event.item;
  }

  onClientSelect(event: any) {
    this.formData.clientId = event.item;
  }

  formatter = (x: { name: string }) => x.name;
}
