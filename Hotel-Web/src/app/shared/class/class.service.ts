import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { retry, catchError, map } from 'rxjs/operators';
import { SessionStore } from 'src/app/store';
import { ClassSession } from '.';

@Injectable({
  providedIn: 'root',
})
export class ClassService {
  constructor(private httpClient: HttpClient) {}

  addClass(classSession: ClassSession): Observable<any> {
    return this.httpClient.post('class', classSession);
  }
}
