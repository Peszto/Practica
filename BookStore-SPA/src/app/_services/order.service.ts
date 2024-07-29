import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Order } from '../_models/Order';
import { ApiResponse } from '../_models/ApiResponse';
import { OrderWithClientAndBookName } from '../_models/OrderWithClientAndBookName';
@Injectable({
  providedIn: 'root',
})
export class OrderService {
  private baseUrl: string = environment.baseUrl + 'api/';

  constructor(private http: HttpClient) {}

  public addOrder(order: Order): Observable<ApiResponse> {
    return this.http.post<ApiResponse>(`${this.baseUrl}orders`, order);
  }

  public updateOrder(id: number, order: Order): Observable<ApiResponse> {
    return this.http.put<ApiResponse>(this.baseUrl + 'orders/' + id, order);
  }

  public getOrders(): Observable<OrderWithClientAndBookName[]> {
    return this.http.get<OrderWithClientAndBookName[]>(this.baseUrl + 'orders');
  }

  public getOrderById(id: number): Observable<OrderWithClientAndBookName> {
    return this.http.get<OrderWithClientAndBookName>(this.baseUrl + 'orders/' + id);
  }

  public deleteOrder(id: number) : Observable<ApiResponse>{
    return this.http.delete<ApiResponse>(this.baseUrl + 'orders/' + id);
  }
}
