import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-order-edit',
  templateUrl: './order-edit.component.html',
  styleUrl: './order-edit.component.css'
})
export class OrderEditComponent implements OnInit {

  constructor (private activeModal: NgbActiveModal) {}

  ngOnInit() {}

  public decline() {
    this.activeModal.close(false);
  }

  public accept() {
    this.activeModal.close(true);
  }

  public dismiss() {
    this.activeModal.dismiss();
  }

  
}
