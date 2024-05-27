import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  CanActivateChildFn,
  CanActivateFn,
  Router,
  RouterStateSnapshot,
} from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { AccountService } from '../public/account/account.service';
import { lastValueFrom, Observable, switchMap, tap } from 'rxjs';
import { AppRoles } from '../public/account';

@Injectable({
  providedIn: 'root',
})
export class AuthGuard {
  constructor(
    private router: Router,
    private jwtHelper: JwtHelperService,
    private http: HttpClient,
    private authService: AccountService
  ) {}

  async validateSession() {
    const tokenValid = await this.isJwtTokenValid();
    if (tokenValid) {
      return true;
    }

    this.authService.clearSession();
    return this.router.navigate(['login']);
  }

  isJwtTokenValid() {
    if (this.hasTokenExpired()) {
      return this.tryRefreshingTokens();
    }
    return new Observable((sub) => sub.next(true));
  }
  hasTokenExpired(): boolean {
    const token: string | null = this.authService.getAccessToken();
    return this.jwtHelper.isTokenExpired(token);
  }

  private tryRefreshingTokens() {
    return this.authService.refreshToken();
  }
}

export const hasInstructorRole: CanActivateFn = (
  route: ActivatedRouteSnapshot,
  state: RouterStateSnapshot
) => {
  return hasRole(AppRoles.Instructor);
};

export const hasAdminRole: CanActivateFn = (
  route: ActivatedRouteSnapshot,
  state: RouterStateSnapshot
) => {
  return hasRole(AppRoles.Admin);
};
export const hasStudentRole: CanActivateFn = (
  route: ActivatedRouteSnapshot,
  state: RouterStateSnapshot
) => {
  return hasRole(AppRoles.Student);
};

const hasRole = (role: AppRoles) => {
  const accountService = inject(AccountService);
  const hasRole = accountService.userHasRole(role);
  if (!hasRole) {
    accountService.$sessionExpired.next(null);
  }
  console.log('hasRole', { role, hasRole });
  return hasRole;
};

export const canAccessReports: CanActivateFn = (
  route: ActivatedRouteSnapshot,
  state: RouterStateSnapshot
) => {
  const accountService = inject(AccountService);
  const result = accountService.userHasAnyRole([
    AppRoles.Admin,
    AppRoles.Instructor,
    AppRoles.Student,
  ]);
  if (!result) {
    accountService.$sessionExpired.next(null);
  }
  console.log('canAccessReports', result);
  return result;
};

export const AuthCheckGuard: CanActivateFn = (
  route: ActivatedRouteSnapshot,
  state: RouterStateSnapshot
) => {
  const accountService = inject(AccountService);

  return inject(AuthGuard)
    .isJwtTokenValid()
    .pipe(
      tap((exp) => {
        if (!exp) {
          accountService.$sessionExpired.next(null);
        }
      })
    );
};
