import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { catchError, debounceTime } from 'rxjs/operators';
import { of } from 'rxjs';
import {
  FormGroup,
  FormBuilder,
  Validators,
  AbstractControl,
} from '@angular/forms';
import { MatSnackBar, MatSnackBarRef } from '@angular/material/snack-bar';
import { StudentsignupService } from './studentsignup.service';
import {
  StudentPaymentSignUp,
  StudentSignup,
} from '../model/studentSignup.model';
import { ActivatedRoute, Router } from '@angular/router';
import { BadReqeustResponse } from 'src/app/shared/badRequest';

@Component({
  selector: 'signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css'],
})
export class SignupComponent {
  form: FormGroup;
  submitted = false;
  referenceId: string | undefined;
  data: StudentPaymentSignUp | undefined;

  constructor(
    private fb: FormBuilder,
    private studentsignupService: StudentsignupService,
    private _snackBar: MatSnackBar,
    private router: Router,
    private route: ActivatedRoute,
    private http: HttpClient
  ) {
    this.form = this.createForm();
  }

  ngOnInit(): void {
    this.route.queryParams.subscribe((params) => {
      this.referenceId = params['id'];
      if (this.referenceId) {
        this.loadStudentData();
      }
    });

    this.f['classCode'].valueChanges.pipe(debounceTime(500)).subscribe((x) => {
      this.f['classCode'].setErrors(null);
    });
  }

  private createForm(): FormGroup {
    debugger;
    return this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      classCode: ['', Validators.required],
      institute: ['', Validators.required],
      password: this.referenceId ? ['', this.getPasswordValidators()] : [''],
    });
  }

  private getPasswordValidators() {
    const validators = [
      Validators.minLength(8),
      Validators.maxLength(20),
      Validators.required,
    ];
    if (this.referenceId) {
      validators.push(Validators.required);
    }
    return validators;
  }

  get f(): { [key: string]: AbstractControl } {
    return this.form.controls;
  }

  onSubmit(): void {
    debugger;
    this.submitted = true;
    if (this.form.invalid) {
      return;
    }
    const sigup: StudentPaymentSignUp = {
      firstName: this.form.value.firstName,
      lastName: this.form.value.lastName,
      email: this.form.value.email,
      classCode: this.form.value.classCode,
      institute: this.form.value.institute,
      password: this.form.value.password,
      reference: this.referenceId!,
      totalAmount:'',
    };
    if (!this.referenceId) {
      this.registerStudent(sigup);
    } else {
      this.studentAccount(sigup);
    }
  }

  private registerStudent(sigup: StudentPaymentSignUp) {
    this.studentsignupService.RegisterAccount(sigup).subscribe({
      next: (x: { id: any }) => {
        this.router.navigate([`payment/payment-initiated/${x.id}`]);
        this._snackBar.open('Student SignUp Successfully');
      },
      error: (msg) => this.handleApiError(msg),
    });
  }

  private handleApiError(errorResponse: any) {
    if (errorResponse.status === 400) {
      const errorMessage = errorResponse.error as BadReqeustResponse;
      for (const [field, error] of Object.entries(errorMessage)) {
        this.form.controls[field].setErrors({ apiError: error });
      }
    }
  }

  private studentAccount(sigup: StudentPaymentSignUp) {
    this.studentsignupService.StudentAccount(sigup).subscribe({
      next: (x: any) => {
        this._snackBar.open('Student SignUp Successfully'),
          this.router.navigate(['login']);
      },
      error: (msg) => this.handleApiError(msg),
    });
  }

  private loadStudentData() {
    this.studentsignupService.getStudent(this.referenceId!).subscribe({
      next: (data: StudentPaymentSignUp) => {
        this.form.patchValue(data);
      },
      error: (error) => {
        if (error.status === 400) {
          this.router.navigate(['login']);
        }
      },
    });
  }

  onReset(): void {
    this.submitted = false;
    this.form.reset();
  }
}
