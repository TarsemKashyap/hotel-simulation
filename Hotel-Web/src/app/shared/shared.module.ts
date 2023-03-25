import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SessionStore } from '../store/session.store';
import { JwtHelperService } from '@auth0/angular-jwt';
import { ClassModule } from './class/class.module';
import {  FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';


@NgModule({
  declarations: [
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    HttpClientModule
  ],
  exports: [ClassModule],
  providers: [SessionStore, JwtHelperService]
})
export class SharedModule { }
