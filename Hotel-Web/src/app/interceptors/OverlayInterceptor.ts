import { Injectable } from '@angular/core';
import {
    HttpRequest,
    HttpHandler,
    HttpEvent,
    HttpInterceptor, HttpEventType
} from '@angular/common/http';
import { Observable, tap } from 'rxjs';
import { OverlayService } from '../shared/overlay.service';


@Injectable()
export class OverlayInterceptor implements HttpInterceptor {
    constructor(private accountService: OverlayService) { }

    intercept(
        request: HttpRequest<any>,
        next: HttpHandler
    ): Observable<HttpEvent<any>> {
        const dialog = this.accountService.open('loading data');

        return next.handle(request).pipe(
            tap({
                next: (e: HttpEvent<any>) => {
                    console.log('next handle', e.type, e);
                    if (e.type == HttpEventType.Response) {
                        dialog.close();
                    }
                },
            })
        );
    }
}
