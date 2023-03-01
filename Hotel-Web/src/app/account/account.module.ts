import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent, SignupComponent } from "./index"
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [LoginComponent, SignupComponent],
  imports: [
    CommonModule,
    ReactiveFormsModule
  ]
})
export class AccountModule { }
