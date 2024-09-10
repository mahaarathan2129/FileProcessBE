import {
  HttpErrorResponse,
  HttpEvent,
  HttpHandler,
  HttpHeaders,
  HttpInterceptor,
  HttpRequest
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable, tap } from 'rxjs';
import { environment } from 'src/app/environment/environment';
import { GlobalService } from 'src/app/services/global.service';
@Injectable()
export class CommonInterceptor implements HttpInterceptor {

  constructor(private router:Router,private global:GlobalService,private toastr:ToastrService) { }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {

    var headers_object: any = new HttpHeaders().set("Authorization", "Bearer " + this.global.getLocalStorage('Token'));

    var interceptUrl: any = request.clone({
      headers: headers_object, url: environment.apiUrl + request.url
    })
    return next.handle(interceptUrl).pipe(tap(() => { },
      (err: any) => {
        if (err instanceof HttpErrorResponse) {
          if (err.status == 401) {
            this.toastr.error('Session Expired');
            this.router.navigate(['/auth/login']);
            return;
          }
        }
      }));
  }
}
