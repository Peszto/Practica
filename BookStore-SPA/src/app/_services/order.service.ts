import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Order } from '../_models/Order';
import { SuccessResponse } from '../_models/SuccessResponse';
@Injectable({
  providedIn: 'root',
})
export class OrderService {
  private baseUrl: string = environment.baseUrl + 'api/';

  constructor(private http: HttpClient) {}

  public addOrder(order: Order): Observable<SuccessResponse> {
    return this.http.post<SuccessResponse>(`${this.baseUrl}orders`, order);
  }

  public updateOrder(id: number, order: Order): Observable<SuccessResponse> {
    return this.http.put<SuccessResponse>(this.baseUrl + 'orders/' + id, order);
  }

  public getOrders(): Observable<Order[]> {
    return this.http.get<Order[]>(this.baseUrl + 'orders');
  }

  public getOrderById(id: number): Observable<Order> {
    return this.http.get<Order>(this.baseUrl + 'orders/' + id);
  }

  public deleteOrder(id: number) {
    return this.http.delete(this.baseUrl + 'orders/' + id);
  }
}
