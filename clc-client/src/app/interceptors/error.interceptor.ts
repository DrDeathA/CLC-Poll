import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { catchError } from 'rxjs/operators';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

  constructor(private router: Router, readonly toastr: ToastrService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
      catchError((error: HttpErrorResponse) => {
        if (error) {
          switch (error.status) {
            case 400:
              this.toastr.warning(error.error);
              break;
            case 401:
              this.toastr.error('Please Login To Gain Access!');
              this.router.navigateByUrl('login');
              break;
              case 404:
                this.toastr.error('Page Not Found');
                this.router.navigateByUrl('polls');
                break;
            default:
              this.toastr.error("Unexpected Error, Please Try Again!");
              break;
          }
        }
        throw error;
      })
    );
  }
}
