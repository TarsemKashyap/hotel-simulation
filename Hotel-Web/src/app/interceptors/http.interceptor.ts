import { Injectable } from '@angular/core';
import {
  HttpEvent,
  HttpInterceptor,
  HttpHandler,
  HttpRequest,
  HttpErrorResponse,
} from '@angular/common/http';
import {
  BehaviorSubject,
  catchError,
  filter,
  map,
  Observable,
  of,
  switchMap,
  take,
  tap,
  throwError,
} from 'rxjs';
import { AccountService } from '../public/account';
import { Router } from '@angular/router';

@Injectable()
export class RefreshTokennterceptor implements HttpInterceptor {
  private isRefreshing = false;
  private refreshTokenSubject: BehaviorSubject<any> = new BehaviorSubject<any>(
    null
  );
  constructor(private accountService: AccountService, private router: Router) {}
  intercept(req: HttpRequest<any>, next: HttpHandler) {
    return next.handle(req).pipe(
      catchError((error) => {
        console.log('Http Interceptor', error);
        if (error.status == 401) {
          return this.handle401Error(next, req);
        }
        return throwError(error);
      })
    );
  }

  private addToken(request: HttpRequest<any>, token: string) {
    return request.clone({
      setHeaders: {
        Authorization: `Bearer ${token}`,
      },
    });
  }
  private handle401Error(next: HttpHandler, request: HttpRequest<any>) {
    if (!this.isRefreshing) {
      this.isRefreshing = true;
      this.refreshTokenSubject.next(null);
console.log("hello");
      this.accountService
        .refreshToken()
        .pipe(
          tap((t) => console.log('refreshToken-top', t)),
          switchMap((token: any) => {
            console.log('Switch Map', token);
            this.isRefreshing = false;
            this.refreshTokenSubject.next(token.accessToken);
            return next.handle(this.addToken(request, token.accessToken));
          }),
          catchError((er) => {
            console.log('session expired');
            this.accountService.$sessionExpired.next(null);
            return throwError(() => er);
          })
        )
        .subscribe((ee) => console.log(ee));
      return next.handle(request);
    } else {
      return this.refreshTokenSubject.pipe(
        filter((token) => token != null),
        take(1),
        switchMap((jwt) => {
          return next.handle(this.addToken(request, jwt));
        })
      );
    }
  }
}
