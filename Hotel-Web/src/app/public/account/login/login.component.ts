import { Component, OnInit } from '@angular/core';
import {
  FormGroup,
  FormBuilder,
  Validators,
  AbstractControl,
} from '@angular/forms';
import { AccountService } from '../account.service';
import { AppRoles, LoginModel, Signup } from '../../student/model/signup.model';
import { MatSnackBar, MatSnackBarRef } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent {
  form: FormGroup;
  submitted = false;
  errorMessage: string | undefined;

  constructor(private fb: FormBuilder, private accountService: AccountService) {
    this.form = this.createForm();
  }

  ngOnInit(): void {}

  private createForm(): FormGroup {
    return this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: [
        '',
        [
          Validators.required,
          Validators.minLength(4),
          Validators.maxLength(30),
        ],
      ],
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
    const login: LoginModel = {
      userId: this.form.value.email,
      password: this.form.value.password,
    };
    this.accountService.login(login).subscribe({
      next: (data) => {
        return this.accountService.redirectToDashboard();
      },
      error: (err) => {
        this.errorMessage = Object.values<string>(err.error).at(0);
      },
    });
  }

  onReset(): void {
    this.submitted = false;
    this.form.reset();
  }
}
