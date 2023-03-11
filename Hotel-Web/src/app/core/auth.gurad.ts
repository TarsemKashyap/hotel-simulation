import { HttpClient, HttpHeaders } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, CanActivateFn, Router, RouterStateSnapshot } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { SessionStore } from '../store/session.store';

@Injectable({
    providedIn: 'root'
})
export class AuthGuard {

    constructor(
        private router: Router,
        private jwtHelper: JwtHelperService,
        private http: HttpClient,
        private sessionStore: SessionStore
    ) { }

    async validateSession() {
        const token: string | null = this.sessionStore.GetAccessToken();

        if (token && !this.jwtHelper.isTokenExpired(token)) {
            console.log(this.jwtHelper.decodeToken(token))
            return true;
        }

        const isRefreshSuccess = await this.tryRefreshingTokens(token!);
        if (!isRefreshSuccess) {
            this.router.navigate(["login"]);
        }

        return isRefreshSuccess;
    }

    private async tryRefreshingTokens(token: string): Promise<boolean> {
        const refreshToken: string | null = this.sessionStore.GetRefreshToken();
        if (!token || !refreshToken) {
            return false;
        }

        const credentials = JSON.stringify({ accessToken: token, refreshToken: refreshToken });
        let isRefreshSuccess: boolean;

        const refreshRes = await new Promise<any>((resolve, reject) => {
            this.http.post<any>("token/refresh", credentials).subscribe({
                next: (res: any) => resolve(res),
                error: (_) => { reject; isRefreshSuccess = false; }
            });
        });

        this.sessionStore.SetAccessToken(refreshRes.token);
        this.sessionStore.SetRefreshToken(refreshRes.refreshToken);
        isRefreshSuccess = true;

        return isRefreshSuccess;
    }
}



export const canActivateHome: CanActivateFn =
    (route: ActivatedRouteSnapshot, state: RouterStateSnapshot) => {
        return inject(AuthGuard).validateSession();
    };