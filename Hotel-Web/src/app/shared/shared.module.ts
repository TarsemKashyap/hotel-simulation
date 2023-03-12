import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SessionStore } from '../store/session.store';
import { JwtHelperService } from '@auth0/angular-jwt';


@NgModule({
  declarations: [
  ],
  imports: [
    CommonModule,
  ],
  exports: [],
  providers: [SessionStore, JwtHelperService]
})
export class SharedModule { }
