import { HttpClient, HttpHeaders } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, CanActivateFn, Router, RouterStateSnapshot } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { AccountService } from '../public/account/account.service';
import { SessionStore } from '../store/session.store';
import { Observable } from 'rxjs';


@Injectable({
    providedIn: 'root'
})
export class AuthGuard {

    constructor(
        private router: Router,
        private jwtHelper: JwtHelperService,
        private http: HttpClient,
        private authService: AccountService
    ) { }

    async validateSession() {
        const token: string | null = this.authService.getAccessToken();
        if (token && !this.jwtHelper.isTokenExpired(token)) {
            console.log(this.jwtHelper.decodeToken(token))
            return true;
        }

        const isRefreshSuccess = await this.tryRefreshingTokens();
        if (!isRefreshSuccess) {
            this.router.navigate(["login"]);
        }

        return isRefreshSuccess;
    }

    private async tryRefreshingTokens(): Promise<boolean> {

        return await new Promise<any>((resolve, reject) => {
            this.authService.refreshToken().subscribe(x => resolve(x), err => reject(err));
        });

    }
}



export const canActivateHome: CanActivateFn =
    (route: ActivatedRouteSnapshot, state: RouterStateSnapshot) => {
        return inject(AuthGuard).validateSession();
    };