import { Component } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { StudentsignupService } from '../../signup/studentsignup.service';
import { StudentPaymentSignUp, StudentSignup } from '../../model/studentSignup.model';

@Component({
  selector: 'app-payment-initiated-page',
  templateUrl: './payment-initiated-page.component.html',
styleUrls: ['./payment-initiated-page.component.css'],

})
export class PaymentInitiatedPageComponent {
  studentId: number | undefined;
  data: StudentPaymentSignUp| undefined;

  constructor(
    private _snackBar: MatSnackBar,
    private route: ActivatedRoute,
    private studentsignupService: StudentsignupService

  ) {
  }

  ngOnInit(): void {
    this.studentId = this.route.snapshot.params['id'];
    this.loadStudentData();
  }

  private loadStudentData() {
    this.studentsignupService.getStudentData(this.studentId!).subscribe({
      next: (data: StudentPaymentSignUp) => {
        this.data = data;
      },
      error: (err) => {
        this._snackBar.open(err.error);
      },
    });
  }

  onSubmit(){

  }
}