import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent, SignupComponent } from "./index"
import { ReactiveFormsModule } from '@angular/forms';
import { AccountService } from './account.service';

@NgModule({
  declarations: [LoginComponent, SignupComponent],
  imports: [
    CommonModule,
    ReactiveFormsModule
  ],
  providers:[AccountService]
})
export class AccountModule { }
