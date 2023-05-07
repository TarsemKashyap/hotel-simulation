import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { catchError } from 'rxjs/operators';
import { of } from 'rxjs';
import {
  FormGroup,
  FormBuilder,
  Validators,
  AbstractControl,
} from '@angular/forms';
import { MatSnackBar, MatSnackBarRef } from '@angular/material/snack-bar';
import { StudentsignupService } from './studentsignup.service';
import { StudentPaymentSignUp, StudentSignup } from '../model/studentSignup.model';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css'],
})
export class SignupComponent {
  form: FormGroup;
  submitted = false;
  referenceId: string | undefined;
  data: StudentPaymentSignUp| undefined;

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
    this.route.queryParams.subscribe(params => {
      this.referenceId = params['id'];
      if(this.referenceId) {
        this.loadStudentData();
      }
    });
  }

  private createForm(): FormGroup {
    debugger
    return this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      classCode: ['', Validators.required],
      institute: ['', Validators.required],
      password: this.referenceId ? ['', this.getPasswordValidators()] : ['']
    });
  }

  private getPasswordValidators() {
    const validators = [Validators.minLength(8), Validators.maxLength(20),Validators.required];
    if (this.referenceId) {
      validators.push(Validators.required);
    }
    return validators;
  }


  get f(): { [key: string]: AbstractControl } {
    return this.form.controls;
  }

  onSubmit(): void {
    debugger
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
      password : this.form.value.password,
      reference: this.referenceId!
    };
    if(!this.referenceId) {
      debugger
      this.studentsignupService.RegisterAccount(sigup).subscribe({ next:(x)=>{this.router.navigate([`Payment/payment-initiated-page/${x.id}`])
      this._snackBar.open('Student SignUp Successfully');},
      error:(error)=>{
        const errorMessage = error.message;
        if(error.status === 400)
        {
          const errorResponse = error.error;
          this.form.controls.firstName.setErrors({ apiError: errorResponse.firstName });
          this.form.controls.lastName.setErrors({ apiError: errorResponse.lastName });
          this.form.controls.email.setErrors({ apiError: errorResponse.email });
          this.form.controls.classCode.setErrors({ apiError: errorResponse.classCode });
          this.form.controls.institute.setErrors({ apiError: errorResponse.institute });
        } else {
          this.form.setErrors({ apiError: errorMessage });
        }
        return of();
      } 
    })
    }
  else{
    this.studentsignupService.StudentAccount(sigup).subscribe({ next:(x)=>{this._snackBar.open('Student SignUp Successfully'),
    this.router.navigate(['login'])},
    error:(error)=>{
      const errorMessage = error.message;
      if(error.status === 400)
      {
        const errorResponse = error.error;
        this.form.controls.firstName.setErrors({ apiError: errorResponse.firstName });
        this.form.controls.lastName.setErrors({ apiError: errorResponse.lastName });
        this.form.controls.email.setErrors({ apiError: errorResponse.email });
        this.form.controls.classCode.setErrors({ apiError: errorResponse.classCode });
        this.form.controls.institute.setErrors({ apiError: errorResponse.institute });
        this.form.controls.password.setErrors({ apiError: errorResponse.password });
      } else {
        this.form.setErrors({ apiError: errorMessage });
      }
      return of();
    } 
  })
  }
  }


  private loadStudentData() {
    this.studentsignupService.getStudent(this.referenceId!).subscribe({
      next: (data: StudentPaymentSignUp) => {
        this.form.patchValue(data);
      },
      error: (error) => {
        if(error.status === 400)
        {
          this.router.navigate(['login'])
        }
      },
    });
  }

  onReset(): void {
    this.submitted = false;
    this.form.reset();
  }
}
