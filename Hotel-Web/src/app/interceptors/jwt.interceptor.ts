import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpResponse,
  HttpEventType,
} from '@angular/common/http';
import { Observable, tap } from 'rxjs';
import { AccountService } from '../public/account/account.service';
import { SessionStore } from '../store';
import { environment } from 'src/environments/environment';
import { OverlayService } from '../shared/overlay.service';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {
  constructor(private accountService: SessionStore) {}

  intercept(
    request: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    // add auth header with jwt if account is logged in and request is to the api url
    const isLoggedIn = this.accountService.GetAccessToken();
    const isApiUrl = request.url.startsWith(environment.apiUrl);
    if (isLoggedIn && isApiUrl) {
      request = request.clone({
        setHeaders: { Authorization: `Bearer ${isLoggedIn}` },
      });
    }

    return next.handle(request);
  }
}

@Injectable()
export class OverlayInterceptor implements HttpInterceptor {
  constructor(private accountService: OverlayService) {}

  intercept(
    request: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    const dialog = this.accountService.open('loading data');

    return next.handle(request).pipe(
      tap({
        next: (e: HttpEvent<any>) => {
          console.log('next handle', e.type, e);
          if (e.type == HttpEventType.Response) {
            dialog.close();
          }
        },
      })
    );
  }
}
