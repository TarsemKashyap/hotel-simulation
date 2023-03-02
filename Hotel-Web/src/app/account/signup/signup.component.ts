import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, AbstractControl } from '@angular/forms';
import { AccountService } from '../account.service';
import { Signup } from '../model/signup.model';
@Component({
  selector: 'signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent {
  form: FormGroup;
  submitted = false;

  constructor(private fb: FormBuilder, private accountService: AccountService) {
    this.form = this.createForm();
  }

  ngOnInit(): void {


  }

  private createForm(): FormGroup {

    return this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(30)]]
    });
  }

  get f(): { [key: string]: AbstractControl } {
    return this.form.controls;
  }

  onSubmit(): void {
   // this.submitted = true;
    if (this.form.invalid) {
      return;
    }
    console.log("REgister objct", { vv: this.form.value });
    const sigup: Signup = {
      FirstName: this.form.value.firstName,
      LastName: this.form.value.lastName,
      Email: this.form.value.email,
      Password: this.form.value.password
    };
    this.accountService.CreateAccount(sigup).subscribe(x => console.log(x));

  }

  onReset(): void {
    this.submitted = false;
    this.form.reset();
  }
}
