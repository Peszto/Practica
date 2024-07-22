import { Injectable } from "@angular/core";
import { environment } from "../../environments/environment";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { Order } from "../_models/Order";

@Injectable({
    providedIn: 'root'
})

export class OrderService{
    private baseUrl :string = environment.baseUrl + 'api/';

    constructor(private http:HttpClient){}
    
    public addOrder(order: Order){
        return this.http.post(this.baseUrl+ 'orders', order);
    }

    public updateOrder(id:number, order:Order){
        return this.http.put(this.baseUrl+'orders/'+id,order);
    }

    public getOrder() : Observable<Order[]>{
        return this.http.get<Order[]>(this.baseUrl + 'orders');
    }

    public deleteOrder(id: number){
        return this.http.delete(this.baseUrl + 'orders/' + id);
    }
}