import { Component, OnInit } from '@angular/core';
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
  ) {
    this.form = this.createForm();
  }

  ngOnInit(): void {
    debugger
    this.route.queryParams.subscribe(params => {
      this.referenceId = params['id'];
      if(this.referenceId) {
        this.loadStudentData();
      }
    });
  }

  private createForm(): FormGroup {
    return this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      classCode: ['', Validators.required],
      institute: ['', Validators.required],
      password: [''],
    });
  }

  get f(): { [key: string]: AbstractControl } {
    return this.form.controls;
  }

  onSubmit(): void {
    this.submitted = true;
    if (this.form.invalid) {
      return;
    }
    const sigup: StudentSignup = {
      firstName: this.form.value.firstName,
      lastName: this.form.value.lastName,
      email: this.form.value.email,
      classCode: this.form.value.classCode,
      institute: this.form.value.institute,
      password : this.form.value.password,
      reference: this.referenceId!
    };
    if(!this.referenceId) {
      this.studentsignupService.RegisterAccount(sigup).subscribe((x) => {
        this.router.navigate([`Paypal/paypal-initiated-page/${x.id}`])
        this._snackBar.open('Student SignUp Successfully');
      });
    }
  else{
    this.studentsignupService.StudentAccount(sigup).subscribe((x) => {
      this._snackBar.open('Student SignUp Successfully');
      this.router.navigate(['login']);
    });
  }
  }


  private loadStudentData() {
    debugger
    this.studentsignupService.getStudent(this.referenceId!).subscribe({
      next: (data: StudentPaymentSignUp) => {
        debugger
        this.form.patchValue(data);
      },
      error: (err) => {
        this._snackBar.open(err.error);
      },
    });
  }

  onReset(): void {
    this.submitted = false;
    this.form.reset();
  }
}
