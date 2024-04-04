import { Injectable } from '@angular/core';
import {
  HttpEvent,
  HttpInterceptor,
  HttpHandler,
  HttpRequest,
  HttpErrorResponse,
} from '@angular/common/http';
import { catchError, map, Observable, switchMap, throwError } from 'rxjs';
import { AccountService } from '../public/account';
import { Router } from '@angular/router';
import { coerceStringArray } from '@angular/cdk/coercion';

@Injectable()
export class RefreshTokennterceptor implements HttpInterceptor {
  constructor(private accountService: AccountService, private router: Router) {}
  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    return next.handle(req).pipe(
      catchError((error) => {
        console.log('Refresh Interceptor', error);
        if (error instanceof HttpErrorResponse) {
         // this.refreshToken(next, req);
        }
        return throwError(() => error);
      })
    );
  }

  private refreshToken(next: HttpHandler, req: HttpRequest<any>) {
    this.accountService
      .refreshToken()
      .pipe(
        switchMap((d) => {
          return next.handle(req);
        }),
        catchError((error) => {
          return this.router.navigate(['/', 'login']);
        })
      )
      .subscribe({
        next: (res) => {
          // return next.handle(req);
        },
        error: (err) => {
          console.log('subscribe::', err);
        },
      });
  }
}
