import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Observable, throwError } from 'rxjs';
import { retry, catchError } from 'rxjs/operators';
import { ChangePasswordRequest } from './change-password.model';


@Injectable({
  providedIn: 'root'
})
export class ChangePasswordService {

  constructor(private httpClient: HttpClient) { }

  private httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
    }),
  };

  changePassword(signup: ChangePasswordRequest): Observable<any> {
    return this.httpClient.post<ChangePasswordRequest>("account/change-password", signup);
  }


}