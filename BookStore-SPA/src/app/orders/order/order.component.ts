import { Component, OnInit } from '@angular/core';
import { Order } from '../../_models/Order';
import { OrderService } from '../../_services/order.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { NgForm } from '@angular/forms';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';
import { BookService } from '../../_services/book.service';
import { ClientService } from '../../_services/client.service';
import { Observable } from 'rxjs';

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

    // this.getBooks();
    // this.getClients();
  }

  // private getBooks() {
  //   this.bookService.getBooks().subscribe(
  //     (books) => {
  //       this.books = books;
  //       console.log(this.books);
  //     },
  //     (err) => {
  //       this.toastr.error('An error occurred while getting the books.');
  //     }
  //   );
  // }

  // private getClients() {
  //   this.clientService.getClients().subscribe(
  //     (clients) => {
  //       this.clients = clients;
  //       this.clients.forEach(
  //         (client: { name: string; firstName: any; lastName: any }) =>
  //           (client.name = `${client.firstName} ${client.lastName}`)
  //       );
  //     },
  //     (err) => {
  //       this.toastr.error('An error occured while getting the clients');
  //     }
  //   );
  // }

  public onSubmit(form: NgForm) {
    console.log(form.form.value);    
    if (form.value.id === 0) {
      form.value.bookId = form.value.bookId.id;
      form.value.clientId = form.value.clientId.id;
      this.insertOrder(form);
    } else {
      this.updateOrder(form);
    }
  }

  private insertOrder(form: NgForm) {
    this.service.addOrder(form.form.value).subscribe(
      () => {
        this.toastr.success('Order placed successfully!');
        this.resetForm(form);
        this.router.navigate(['/orders']);
      },
      () => {
        this.toastr.error('An error occured while placing the order.');
      }
    );
  }

  private updateOrder(form: NgForm) {
    this.service.updateOrder(form.form.value.id, form.form.value).subscribe(
      () => {
        this.toastr.success('Update successfull!');
        this.resetForm(form);
        this.router.navigate(['/orders']);
      },
      () => {
        this.toastr.error('An error occured on update of the order.');
      }
    );
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
      debounceTime(200),
      distinctUntilChanged(),
      map(term =>
        term.length < 2 ? [] : this.books.filter((v: { name: string; }) => v.name.toLowerCase().indexOf(term.toLowerCase()) > -1).slice(0, 10)
      )
    );

  searchClients = (text$: Observable<string>) =>
    text$.pipe(
      debounceTime(200),
      distinctUntilChanged(),
      map(term =>
        term.length < 2 ? [] : this.clients.filter((v: { name: string; }) => v.name.toLowerCase().indexOf(term.toLowerCase()) > -1).slice(0, 10)
      )
    );

  formatter = (x: { name: string }) => x.name;
}
