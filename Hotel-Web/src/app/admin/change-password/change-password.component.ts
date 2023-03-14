import { Component } from '@angular/core';
import { FormGroup, FormBuilder, Validators, AbstractControl } from '@angular/forms';
import { ChangePasswordRequest } from './change-password.model';
import { ChangePasswordService } from './change-password.service';

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.css']
})
export class ChangePasswordComponent {
  form: FormGroup;
  submitted = false;

  constructor(private fb: FormBuilder, private accountService: ChangePasswordService) {
    this.form = this.createForm();
  }

  ngOnInit(): void {


  }

  private createForm(): FormGroup {

    return this.fb.group({
      currentPassword: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(30)]],
      newPassword: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(30)]],
      confirmPassword: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(30)]]
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
    const login: ChangePasswordRequest = {
      newPassword: this.form.value.newPassword,
      confirmPassword: this.form.value.confirmPassword,
      currentPassword:this.form.value.currentPassword
    };
    this.accountService.changePassword(login).subscribe(x => {

    });

  }

  onReset(): void {
    this.submitted = false;
    this.form.reset();
  }
}
