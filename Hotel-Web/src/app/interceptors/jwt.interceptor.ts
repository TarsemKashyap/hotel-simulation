import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AccountService } from '../public/account/account.service';
import { SessionStore } from '../store';
import { environment } from 'src/environments/environment';


@Injectable()
export class JwtInterceptor implements HttpInterceptor {
    constructor(private accountService: SessionStore) {
        
     }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
       // add auth header with jwt if account is logged in and request is to the api url
       next.handle(request).subscribe(next: (x: number) => console.log('Observer got a next value: ' + x),
       error: (err: Error) => console.error('Observer got an error: ' + err),
       complete: () => console.log('Observer got a complete notification'),);
        const isLoggedIn = this.accountService.GetAccessToken();
        const isApiUrl = request.url.startsWith(environment.apiUrl);
        if (isLoggedIn && isApiUrl) {
            request = request.clone({
                setHeaders: { Authorization: `Bearer ${isLoggedIn}` }
            });
        }

        return next.handle(request);
    }
}