import { Component, OnInit } from '@angular/core';
import { Client } from '../../_models/Client';
import { ClientService } from '../../_services/client.service';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute, Router } from '@angular/router';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-client',
  templateUrl: './client.component.html',
  styleUrl: './client.component.css'
})
export class ClientComponent implements OnInit {

  public formData! : Client;
  constructor (public service:ClientService,
              private router: Router,
              private route: ActivatedRoute,
              private toastr: ToastrService
  ) {}

  ngOnInit() {
    this.resetForm();

    let id;
    this.route.params.subscribe(params => {
      id = params['id'];
    });

    if (id != null) {
      this.service.getClientById(id).subscribe(client => {
        this.formData = client;
      }, error => {
        this.toastr.error('An error occurred on get the record.');
      });
    } else {
      this.resetForm();
    }
  }

  private resetForm(form? : NgForm){
    if(form!=null){
      form.form.reset();
    }

    this.formData = {
      id: 0,
      firstName: '',
      lastName: '',
      phoneNr: '',
      email: '',
      address: ''
    };

  }

  public onSubmit(form: NgForm){
    if(form.value.id === 0 ){
      this.insertRecord(form);
    } else {
      this.updateRecord(form);
    }
  }

  public insertRecord(form: NgForm) {
    this.service.addClient(form.form.value).subscribe(() => {
      this.toastr.success('Registration successful');
      this.resetForm(form);
      this.router.navigate(['/clients']);
    }, () => {
      this.toastr.error('An error occurred on insert the record.');
    });
  }

  public updateRecord(form: NgForm) {
    this.service.updateClient(form.form.value.id, form.form.value).subscribe(() => {
      this.toastr.success('Updated successful');
      this.resetForm(form);
      this.router.navigate(['/clients']);
    }, () => {
      this.toastr.error('An error occurred on update the record.');
    });
  }

  public cancel() {
    this.router.navigate(['/clients']);
  }
}
