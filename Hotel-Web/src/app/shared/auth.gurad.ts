import { HttpClient, HttpHeaders } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  CanActivate,
  CanActivateFn,
  Router,
  RouterStateSnapshot,
} from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { AccountService } from '../public/account/account.service';
import { SessionStore } from '../store/session.store';
import { lastValueFrom, Observable } from 'rxjs';

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
    console.log('AuthGurad');
    const token: string | null = this.authService.getAccessToken();
    if (token && !this.jwtHelper.isTokenExpired(token)) {
      console.log(this.jwtHelper.decodeToken(token));
      return true;
    }
    return await this.tryRefreshingTokens();
  }

  private async tryRefreshingTokens(): Promise<boolean> {
    return lastValueFrom(this.authService.refreshToken()).then(
      (x) => Promise.resolve(true),
      (error) => {
        return this.router.navigate(['login']);
      }
    );
  }
}

export const canActivateHome: CanActivateFn = (
  route: ActivatedRouteSnapshot,
  state: RouterStateSnapshot
) => {
  return inject(AuthGuard).validateSession();
};
