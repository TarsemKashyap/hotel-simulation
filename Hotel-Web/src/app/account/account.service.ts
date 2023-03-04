import { Injectable } from '@angular/core';
import { Signup } from './model/signup.model';
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Observable, throwError } from 'rxjs';
import { retry, catchError } from 'rxjs/operators';


@Injectable({
  providedIn: 'root'
})
export class AccountService {

  constructor(private httpClient: HttpClient) { }
  
  private httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
    }),
  };

  CreateAccount(signup: Signup): Observable<Signup> {
    return this.httpClient.post<Signup>("account/instructor", signup);
  }
}
//http://localhost:5030/account/instructor