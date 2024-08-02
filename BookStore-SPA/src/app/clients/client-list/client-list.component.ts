import { Component, OnInit } from '@angular/core';
import { debounceTime, Subject } from 'rxjs';
import { ClientService } from '../../_services/client.service';
import { ToastrService } from 'ngx-toastr';
import { ConfirmationDialogService } from '../../_services/confirmation-dialog.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-client-list',
  templateUrl: './client-list.component.html',
  styleUrl: './client-list.component.css',
})
export class ClientListComponent implements OnInit {
  public clients: any;
  public searchTerm!: string;
  public searchValueChanged: Subject<string> = new Subject<string>();

  constructor(
    private router: Router,
    private service: ClientService,
    private toastr: ToastrService,
    private confirmationDialogService: ConfirmationDialogService
  ) {}

  ngOnInit(): void {
    this.getClients();

    this.searchValueChanged.pipe(debounceTime(1000)).subscribe(() => {
      this.search();
    });
  }

  private getClients() {
    this.service.getClients().subscribe((clients) => {
      this.clients = clients;
    });
  }

  public addClient() {
    this.router.navigate(['/client']);
  }

  public editClient(clientId: number) {
    this.router.navigate(['/client/' + clientId]);
  }

  public deleteClient(clientId: number) {
    this.confirmationDialogService
      .confirm('Atention', 'Do you really want to delete this client?')
      .then(() =>
        this.service.deleteClient(clientId).subscribe({
          next: () => {
            this.toastr.success('The client has been deleted');
            this.getClients();
          },
          error: (error) => {
            this.toastr.error('The client cannot be deleted!');
          },
        })
      )
      .catch(() => '');
  }

  public searchClients() {
    this.searchValueChanged.next(this.searchTerm);
  }

  private search() {
    if (this.searchTerm !== '') {
      this.clients = this.clients.filter((client: { firstName: any; lastName: any; }) =>
        `${client.firstName} ${client.lastName}`
          .toLowerCase()
          .includes(this.searchTerm.toLowerCase())
      );
    } else {
      this.service
        .getClients()
        .subscribe((clients) => (this.clients = clients));
    }
  }
}
