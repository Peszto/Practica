import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { debounceTime, Subject } from 'rxjs';
import { ToastrService } from 'ngx-toastr';
import { ConfirmationDialogService } from '../../_services/confirmation-dialog.service';
import { OrderService } from '../../_services/order.service';
import { BookService } from '../../_services/book.service';
import { ClientService } from '../../_services/client.service';

@Component({
  selector: 'app-order-list',
  templateUrl: './order-list.component.html',
  styleUrl: './order-list.component.css',
})
export class OrderListComponent implements OnInit {
  public orders: any;
  public listComplet: any;
  public searchTerm!: string;
  public searchValueChanged: Subject<String> = new Subject<String>();
  public clients: any[] = [];
  public books: any[] = [];

  constructor(
    private router: Router,
    private service: OrderService,
    private toastr: ToastrService,
    private confirmationDialogService: ConfirmationDialogService,
    private bookService: BookService,
    private clientService: ClientService
  ) {}

  ngOnInit() {
    this.getValues();

    this.searchValueChanged.pipe(debounceTime(1000)).subscribe(() => {
      this.search();
    });
  }

  private getValues() {
    this.service.getOrders().subscribe((orders) => {
      this.orders = orders;
      this.listComplet = orders;
      this.fetchBooks();
      this.fetchClients();
    });
  }
  private fetchClients() {
    this.clientService.getClients().subscribe((clients) => {
      this.clients = clients;
      this.addClientNamesToOrders();
    });
  }

  private fetchBooks() {
    this.bookService.getBooks().subscribe((books) => {
      this.books = books;
      this.addBookNamesToOrders();
    });
  }

  private addClientNamesToOrders() {
    this.orders.forEach((order: any) => {
      const client = this.clients.find(
        (client) => client.id === order.clientId
      );
      if (client) {
        order.clientName = client.firstName + ' ' + client.lastName;
      }
    });
  }

  private addBookNamesToOrders() {
    this.orders.forEach((order: any) => {
      const book = this.books.find((book) => book.id === order.bookId);
      if (book) {
        order.bookName = book.name;
      }
    });
  }

  public deleteOrder(orderId: number) {
    this.confirmationDialogService
      .confirm('Atention', 'Do you really want to delete this order?')
      .then(() =>
        this.service.deleteOrder(orderId).subscribe({
          next: () => {
            this.toastr.success('The order has been deleted');
            this.getValues();
          },
          error: () => {
            this.toastr.error('Failed to delete the order.');
          },
        })
      )
      .catch(() => '');
  }

  public addOrder() {
    this.router.navigate(['/order']);
  }

  public editOrder(orderId: number) {
    this.router.navigate(['/order/' + orderId]);
  }

  public searchOrders() {
    this.searchValueChanged.next(this.searchTerm);
  }

  private search() {
    if (this.searchTerm !== '') {
      this.orders = this.orders.filter((order: { orderNr: { toString: () => string | string[]; }; }) =>
        order.orderNr.toString().includes(this.searchTerm)
      );
    } else {
      this.service.getOrders().subscribe((orders) => (this.orders = orders));
    }
  }
}
