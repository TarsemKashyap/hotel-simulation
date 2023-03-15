import { Injectable } from '@angular/core';
import { LoginModel, LoginResponse, Signup } from './model/signup.model';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { retry, catchError, map } from 'rxjs/operators';
import { SessionStore } from 'src/app/store';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  constructor(
    private httpClient: HttpClient,
    private sessionStore: SessionStore
  ) {}

  private httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
    }),
  };

  CreateAccount(signup: Signup): Observable<Signup> {
    return this.httpClient.post<Signup>('account/instructor', signup);
  }

  login(login: LoginModel): Observable<LoginResponse> {
    return this.httpClient.post<LoginResponse>('account/login', login).pipe(
      map((x) => {
        this.sessionStore.SetAccessToken(x.accessToken);
        this.sessionStore.SetRefreshToken(x.refreshToken);
        return x;
      })
    );
  }

  getAccessToken() {
    return this.sessionStore.GetAccessToken();
  }

  refreshToken(): Observable<boolean> {
    const refreshToken = this.sessionStore.GetRefreshToken();
    const token = this.sessionStore.GetAccessToken();
    if (!token || !refreshToken) {
      return new Observable((ob) => ob.next(false));
    }

    const credentials = { accessToken: token, refreshToken: refreshToken };
    return this.httpClient.post<any>('account/token/refresh', credentials).pipe(
      map((x) => {
        this.sessionStore.SetAccessToken(x.accessToken);
        this.sessionStore.SetRefreshToken(x.refreshToken);
        return true;
      })
    );
  }
}
