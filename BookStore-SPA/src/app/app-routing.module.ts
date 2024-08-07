import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { BookListComponent } from './books/book-list/book-list.component';
import { BookComponent } from './books/book/book.component';
import { CategoryListComponent } from './categories/category-list/category-list.component';
import { CategoryComponent } from './categories/category/category.component';
import { OrderComponent } from './orders/order/order.component';
import { OrderListComponent } from './orders/order-list/order-list.component';
import { ClientComponent } from './clients/client/client.component';
import { ClientListComponent } from './clients/client-list/client-list.component';
import { Order } from './_models/Order';

const routes: Routes = [
  { path: 'home', component: HomeComponent },
  
  { path: 'books', component: BookListComponent },
  { path: 'book', component: BookComponent },
  { path: 'book/:id', component: BookComponent },

  { path: 'categories', component: CategoryListComponent },
  { path: 'category', component: CategoryComponent },
  { path: 'category/:id', component: CategoryComponent },

  { path: 'orders', component: OrderListComponent},
  { path: 'order', component: OrderComponent},
  { path: 'order/:id', component: OrderComponent},

  { path: 'client', component: ClientComponent},
  { path: 'clients', component: ClientListComponent},
  { path: 'client/:id', component: ClientComponent},

  { path: '**', redirectTo: 'home', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }


