import { Injectable } from '@angular/core';
import {
  AppRoles,
  InstructorSignup,
  InstructorUpdate,
  LoginModel,
  LoginResponse,
  Signup,
} from '../student/model/signup.model';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, Subject, throwError } from 'rxjs';
import {
  retry,
  catchError,
  map,
  switchMap,
  tap,
  startWith,
  shareReplay,
} from 'rxjs/operators';
import { SessionStore } from 'src/app/store';
import { Router } from '@angular/router';
import { appRoutes } from '../public-routing.module';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  $sessionExpired = new Subject();
  isRefreshing: boolean;
  constructor(
    private httpClient: HttpClient,
    private sessionStore: SessionStore,
    private router: Router
  ) {}

  CreateAccount(signup: Signup): Observable<any> {
    return this.httpClient.post('account/instructor', signup);
  }

  CreateAccountInstructor(signup: InstructorSignup): Observable<any> {
    return this.httpClient.post('account/instructor', signup);
  }

  InstructorUpdate(id: string, update: InstructorUpdate): Observable<any> {
    return this.httpClient.post(`account/instructor/update/${id}`, update);
  }

  login(login: LoginModel): Observable<LoginResponse> {
    return this.httpClient.post<LoginResponse>('account/login', login).pipe(
      map((x) => {
        this.sessionStore.SetAccessToken(x.accessToken);
        this.sessionStore.SetRefreshToken(x.refreshToken);
        this.sessionStore.AddRole(x.roles);
        return x;
      })
    );
  }

  getAccessToken() {
    return this.sessionStore.GetAccessToken();
  }

  refreshToken(): Observable<any> {
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
      }),
      catchError((er, ob) => {
        return new Observable((sub) => sub.next(false));
      })
    );
  }

  refreshToken2(): Observable<any> {
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
        return x;
      }),
      catchError((er, ob) => {
        return new Observable((sub) => sub.next(false));
      })
    );
  }

  userHasRole(role: AppRoles): boolean {
    const savedRole = this.sessionStore.GetRole();
    let b = savedRole.some((x) => x == role);
    return b;
  }

  userHasAnyRole(roles: AppRoles[]): boolean {
    const savedRole = this.sessionStore.GetRole();
    const hasRole = savedRole.some((x) => roles.some((y) => y == x));
    return hasRole;
  }

  getInstructor(userId: string): Observable<InstructorUpdate> {
    return this.httpClient.get<InstructorUpdate>(
      `account/instructor/${userId}`
    );
  }

  clearSession() {
    this.sessionStore.clearSession();
  }

  redirectToDashboard(): Promise<boolean> {
    if (this.userHasRole(AppRoles.Admin)) {
      return this.router.navigate(['/', 'admin']);
    }

    if (this.userHasRole(AppRoles.Student)) {
      return this.router.navigate(['/', 'student']);
    }
    if (this.userHasRole(AppRoles.Instructor)) {
      return this.router.navigate(['/', 'instructor']);
    }
    return this.router.navigate(['/', 'login']);
  }

  isLoggedIn(): boolean {
    return this.sessionStore.GetAccessToken() != null;
  }
}
