import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { StudentSignup } from '../model/studentSignup.model';
import { PaymentTransaction } from '../model/PaymentTransaction.model';

@Injectable({
  providedIn: 'root'
})
export class StudentsignupService {

  constructor(private httpClient: HttpClient) { }

  RegisterAccount(signup: StudentSignup): Observable<any> {
    return this.httpClient.post('signup/studentsignup', signup);
  }


  getStudentData(studentId: number): Observable<any> {
    return this.httpClient.get<StudentSignup[]>(
      `signup/signup/${studentId}`
    );
  }

  PaymentTransaction(paymentTransaction: PaymentTransaction): Observable<any> {
    return this.httpClient.post('signup/paymentCheck', paymentTransaction);
  }

  getStudent(referenceId: string): Observable<any> {
    return this.httpClient.get<StudentSignup[]>(
      `signup/${referenceId}`
    );
  }

  StudentAccount(signup: StudentSignup): Observable<any> {
    return this.httpClient.post('signup/student', signup);
  }
}
