import { Injectable } from '@angular/core';
import {
  HttpEvent,
  HttpInterceptor,
  HttpHandler,
  HttpRequest
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';


@Injectable()
export class APIInterceptor implements HttpInterceptor {
  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    console.log("APIInterceptor");
    const url = `${environment.apiUrl}/${req.url}`;
    console.log('Url', url);
    const apiReq = req.clone({ url: url });
    return next.handle(apiReq);
  }
}
