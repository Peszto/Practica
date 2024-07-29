import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { debounceTime, Subject } from 'rxjs';
import { ToastrService } from 'ngx-toastr';
import { ConfirmationDialogService } from '../../_services/confirmation-dialog.service';
import { OrderService } from '../../_services/order.service';
import { BookService } from '../../_services/book.service';
import { ClientService } from '../../_services/client.service';
import { ApiResponse } from '../../_models/ApiResponse';
import { OrderWithClientAndBookName } from '../../_models/OrderWithClientAndBookName';

@Component({
  selector: 'app-order-list',
  templateUrl: './order-list.component.html',
  styleUrl: './order-list.component.css',
})
export class OrderListComponent implements OnInit {
  public orders!: OrderWithClientAndBookName[];
  public listComplet!: OrderWithClientAndBookName[];
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
      console.log(orders.length);
    });
  }

  public deleteOrder(orderId: number) {
    this.confirmationDialogService
      .confirm('Atention', 'Do you really want to delete this order?')
      .then(() =>
        this.service.deleteOrder(orderId).subscribe({
          next: (response: ApiResponse) => {
            if (response.success) {
              this.toastr.success(response.message);
              this.listComplet = this.listComplet.filter((order:any)=>{ order.id !== orderId});
              this.orders = this.listComplet;
            } else {
              this.toastr.error(response.message);
            }
          },
          error: (err) => {
            this.toastr.error(err.error.message);
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
      this.orders = this.orders.filter(
        (order ) =>
          order.orderNr!.toString().includes(this.searchTerm)
      );
    } else {
      this.service.getOrders().subscribe((orders) => (this.orders = orders));
    }
  }
}
