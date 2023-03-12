import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, AbstractControl } from '@angular/forms';
import { AccountService } from '../account.service';
import { LoginModel, Signup } from '../model/signup.model';
import { MatSnackBar, MatSnackBarRef } from '@angular/material/snack-bar';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  form: FormGroup;
  submitted = false;

  constructor(private fb: FormBuilder, private accountService: AccountService, private _snackBar: MatSnackBar) {
    this.form = this.createForm();
  }

  ngOnInit(): void {


  }

  private createForm(): FormGroup {

    return this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(30)]]
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
      UserId: this.form.value.email,
      Password: this.form.value.password
    };
    this.accountService.login(login).subscribe(x => {
      console.log("Signup", x);
      this._snackBar.open("Instructor Account created");
    });

  }

  onReset(): void {
    this.submitted = false;
    this.form.reset();
  }
}
