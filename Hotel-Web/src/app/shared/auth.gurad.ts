import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  CanActivateFn,
  Router,
  RouterStateSnapshot,
} from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { AccountService } from '../public/account/account.service';
import { lastValueFrom } from 'rxjs';
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

  async validateSession(routeData: AuthRouteData) {
    const tokenValid = await this.isJwtTokenValid();
    const hasRole = this.authService.userHasRole(routeData.role);
    if (tokenValid && hasRole) {
      return true;
    }
    if (tokenValid && !hasRole) {
      return this.authService.redirectToDashboard();
    }
    this.authService.clearSession();
    return this.router.navigate(['login']);
  }

  private async isJwtTokenValid(): Promise<boolean> {
    const token: string | null = this.authService.getAccessToken();
    if (token && this.jwtHelper.isTokenExpired(token)) {
      return await this.tryRefreshingTokens();
    }
    return Promise.resolve(true);
  }

  private async tryRefreshingTokens(): Promise<boolean> {
    return lastValueFrom(this.authService.refreshToken()).then(
      (x) => Promise.resolve(true),
      (error) => Promise.resolve(false)
    );
  }
}

export const checkAccessPermission: CanActivateFn = (
  route: ActivatedRouteSnapshot,
  state: RouterStateSnapshot
) => {
  return inject(AuthGuard).validateSession(route.data as AuthRouteData);
};

export interface AuthRouteData {
  role: AppRoles;
}
