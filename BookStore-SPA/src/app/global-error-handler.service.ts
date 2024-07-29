import { ErrorHandler, Injectable, Injector } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { HttpErrorResponse } from '@angular/common/http';

@Injectable()
export class GlobalErrorHandlerService implements ErrorHandler {
  constructor(private injector: Injector) {}

  handleError(error: any): void {
    const toastr = this.injector.get(ToastrService);

    if (error.error instanceof HttpErrorResponse) {
      // Client-side or network error
      console.error('Client-side error:', error.message);
      toastr.error(error.message);
    } else {
      // Server-side error
      console.error('Server-side error:', error);
      toastr.error(error.message);
     
    }
  }
}
