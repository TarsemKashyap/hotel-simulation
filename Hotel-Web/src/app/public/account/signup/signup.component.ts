import { Component, OnInit } from '@angular/core';
import {
  FormGroup,
  FormBuilder,
  Validators,
  AbstractControl,
} from '@angular/forms';
import { MatSnackBar, MatSnackBarRef } from '@angular/material/snack-bar';
import { StudentsignupService } from './studentsignup.service';
import { StudentSignup } from '../model/studentSignup.model';
import { Router } from '@angular/router';

@Component({
  selector: 'signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css'],
})
export class SignupComponent {
  form: FormGroup;
  submitted = false;

  constructor(
    private fb: FormBuilder,
    private studentsignupService: StudentsignupService,
    private _snackBar: MatSnackBar,
    private router: Router
  ) {
    this.form = this.createForm();
  }

  ngOnInit(): void {}

  private createForm(): FormGroup {
    return this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      ClassCode: ['', Validators.required],
      Institute: ['', Validators.required]
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
      classCode: this.form.value.ClassCode,
      institute: this.form.value.Institute,
    };
    this.studentsignupService.RegisterAccount(sigup).subscribe((x) => {
      debugger
      console.log('Signup', x);
      this.router.navigate([`Paypal/paypal-initiated-page/${x.id}`])
      this._snackBar.open('Student SignUp Successfully');
    });
  }

  onReset(): void {
    this.submitted = false;
    this.form.reset();
  }
}
