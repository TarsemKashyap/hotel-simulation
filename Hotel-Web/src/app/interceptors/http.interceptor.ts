import { Injectable } from '@angular/core';
import {
  HttpEvent,
  HttpInterceptor,
  HttpHandler,
  HttpRequest,
  HttpErrorResponse,
} from '@angular/common/http';
import { catchError, map, Observable, switchMap, throwError } from 'rxjs';
import { environment } from 'src/environments/environment';
import { AccountService } from '../public/account';
import { Router } from '@angular/router';

@Injectable()
export class APIInterceptor implements HttpInterceptor {
  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    const url = `${environment.apiUrl}/${req.url}`;
    console.log('Url', url);
    const apiReq = req.clone({ url: url });
    return next.handle(apiReq);
  }
}

@Injectable()
export class RefreshTokennterceptor implements HttpInterceptor {
  constructor(private accountService: AccountService,private router:Router) {}
  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    return next.handle(req).pipe(
      catchError((error) => {
        console.log('Refresh Interceptor', error);
        if (
          error instanceof HttpErrorResponse &&
          error.status == 401        ) {
          this.refreshToken(next, req);
        }
        return throwError(() => error);
      })
    );
  }

  private refreshToken(next: HttpHandler, req: HttpRequest<any>) {
    this.accountService.refreshToken().pipe(
      switchMap(() => next.handle(req)),
      catchError((error2) => this.router.navigate(["/","login"]))
    );
  }
}
