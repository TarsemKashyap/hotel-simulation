import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BannerComponent } from './banner/banner.component';
import { SessionStore } from '../store/session.store';
import { JwtHelperService } from '@auth0/angular-jwt';
import { AccountModule } from '../account';


@NgModule({
  declarations: [BannerComponent],
  imports: [
    CommonModule,
    AccountModule
  ],
  exports: [BannerComponent],
  providers: [SessionStore, JwtHelperService]
})
export class CoreModule { }
