
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { NavComponent } from './nav/nav.component';
import { BookComponent } from './books/book/book.component';
import { BookListComponent } from './books/book-list/book-list.component';
import { CategoryComponent } from './categories/category/category.component';
import { CategoryListComponent } from './categories/category-list/category-list.component';
import { provideHttpClient, withFetch } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { BookService } from './_services/book.service';
import { CategoryService } from './_services/category.service';
import { ConfirmationDialogComponent } from './confirmation-dialog/confirmation-dialog.component';
import { ConfirmationDialogService } from './_services/confirmation-dialog.service';
import { NgbdDatepickerPopup } from './datepicker/datepicker-popup';
import { OrderComponent } from './orders/order/order.component';
import { OrderListComponent } from './orders/order-list/order-list.component';
import { OrderService } from './_services/order.service';
import { ClientService } from './_services/client.service';
import { ClientComponent } from './clients/client/client.component';
import { ClientListComponent } from './clients/client-list/client-list.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    NavComponent,
    BookComponent,
    BookListComponent,
    CategoryComponent,
    CategoryListComponent,
    ConfirmationDialogComponent,
    NgbdDatepickerPopup,
    OrderComponent,
    OrderListComponent,
    ClientComponent,
    ClientListComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    NgbModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot(),
  ],
  providers: [
    provideHttpClient(withFetch()),
    BookService,
    CategoryService,
    ConfirmationDialogService,
    OrderService,
    ClientService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
