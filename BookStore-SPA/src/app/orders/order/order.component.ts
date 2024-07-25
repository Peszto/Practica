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
import { Observable, of, OperatorFunction, Subject } from 'rxjs';
import { error } from 'console';
import { ApiResponse } from '../../_models/ApiResponse';
import { Server } from 'http';
import { Book } from '../../_models/Book';
import { BasicModel } from '../../_models/BasicModel';

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
  public selectedBookPrice? : number | null;
  private bookSearchTerms = new Subject<string>();
  public model : any;
 

  constructor(
    public service: OrderService,
    private bookService: BookService,
    private clientService: ClientService,
    private router: Router,
    private route: ActivatedRoute,
    private toastr: ToastrService,
    
  ) {}

  ngOnInit() {
    this.resetForm();
    let id;
    this.route.params.subscribe((params) => {
      id = params['id'];
    });

    if (id != null) {
      this.service.getOrderById(id).subscribe(
        (order) => {
          this.formData = order;
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

  searchBooks: OperatorFunction<string, readonly BasicModel[]> = (text$: Observable<string>) =>
    text$.pipe(
      debounceTime(200),
      distinctUntilChanged(),
      switchMap((term) =>
        term.length >= 2 ?
        this.bookService.filterBookNames(term).pipe(
          map((response: BasicModel[] | ApiResponse) => {
            if (Array.isArray(response)) {
              return response;
            } else {
              this.toastr.error(response.message);
              console.log(response);
              return [];
            }
          })
        ) : of([])
      ),
    );

  searchClients: OperatorFunction<string, readonly BasicModel[]> = (text$: Observable<string>) =>
    text$.pipe(
      debounceTime(200),
      distinctUntilChanged(),
      switchMap((term) =>
        term.length >= 2 ?
        this.clientService.filterClientNames(term).pipe(
          map((response: BasicModel[] | ApiResponse) => {
            if (Array.isArray(response)) {
              return response;
            } else {
              this.toastr.error(response.message);
              return [];
            }
          }) 
        ) : of([])
      )
    );

  onBookSelect(event: any) {
    this.bookService.getBookById(event.item.id).subscribe((book)=>{
      this.selectedBookPrice = book.price;
      this.formData.bookId = event.item;
      this.calculateTotalPrice();
    })
  }

  onClientSelect(event: any) {
    this.formData.clientId = event.item;
  }

  onQuantityChange() {
    this.calculateTotalPrice();
  }


  private calculateTotalPrice() {
    if(!this.formData.quantity || this.formData.quantity <=0){
      this.formData.totalPrice = 0;
    }
    else if (this.formData.quantity && this.selectedBookPrice) {
      this.formData.totalPrice = this.formData.quantity * this.selectedBookPrice;
    } 
  }



  // formatter = (result: BasicModel) => result.Name;
  formatter = (x: { name: string }) => x.name;


}
