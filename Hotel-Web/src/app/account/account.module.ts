import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {LoginComponent,SignupComponent} from "./index"


@NgModule({
  declarations: [LoginComponent,SignupComponent],
  imports: [
    CommonModule
  ]
})
export class AccountModule { }
