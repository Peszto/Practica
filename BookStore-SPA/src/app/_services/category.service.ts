import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Category } from '../_models/Category';
import { filter, Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { BasicModel } from '../_models/BasicModel';
import { ApiResponse } from '../_models/ApiResponse';

@Injectable({
  providedIn: 'root',
})
export class CategoryService {
  private baseUrl: string = environment.baseUrl + 'api/';

  constructor(private http: HttpClient) {}

  public addCategory(category: Category) : Observable<ApiResponse> {
    return this.http.post<ApiResponse>(this.baseUrl + 'categories', category);
  }

  public updateCategory(id: number, category: Category) {
    return this.http.put(this.baseUrl + 'categories/' + id, category);
  }

  public getCategories(): Observable<Category[]> {
    return this.http.get<Category[]>(this.baseUrl + `categories`);
  }

  public deleteCategory(id: number) : Observable<ApiResponse> {
    return this.http.delete<ApiResponse>(this.baseUrl + 'categories/' + id);
  }

  public getCategoryById(id: any): Observable<Category> {
    return this.http.get<Category>(this.baseUrl + 'categories/' + id);
  }

  public search(name: string): Observable<Category[]> {
    return this.http.get<Category[]>(
      `${this.baseUrl}categories/search/${name}`
    );
  }

  public filterCategories(filteredValue: string): Observable<BasicModel[]> {
    return this.http.get<BasicModel[]>(
      this.baseUrl + 'category/filter/' + filteredValue
    );
  }
}
