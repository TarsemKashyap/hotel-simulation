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
        if (error.status == 401) {
          this.handle401Error(next, req).subscribe((x) =>
            console.log('handle401Error', x)
          );
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
    this.isRefreshing = false;
    if (!this.isRefreshing) {
      this.isRefreshing = true;
      this.refreshTokenSubject.next(null);
      return this.accountService.refreshToken2().pipe(
        switchMap((token: any) => {
          this.isRefreshing = false;

          this.refreshTokenSubject.next(this.accountService.getAccessToken());
          return next.handle(
            this.addToken(request, this.accountService.getAccessToken()!)
          );
        }),
        catchError((er) => {
          this.accountService.$sessionExpired.next(null);
          return throwError(() => er);
        })
      );
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
