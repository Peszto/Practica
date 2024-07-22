import { Component } from '@angular/core';
import { Order } from '../../_models/Order';
import { OrderService } from '../../_services/order.service';
import { Router } from 'express';
import { ActivatedRoute } from '@angular/router';
import { CategoryService } from '../../_services/category.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrl: './order.component.css'
})
export class OrderComponent {
  public formData!: Order;
  public categories: any;

  constructor(public service: OrderService,
    private categoryService: CategoryService,
    private router: Router,
    private route: ActivatedRoute,
    private toastr: ToastrService) { }

  // ngOnInit(){
  //   this.resetForm();
  //   let id;
  //   this.route.params.subscribe(params=>{
  //     id=params['id'];
  //   });

  
  //}
}
