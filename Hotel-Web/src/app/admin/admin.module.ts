import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DashboardComponent } from './dashboard/admin-dashboard.component';
import { AdminRouteModule } from './admin-route.module';
import { SharedModule } from '../shared/shared.module';
import { ChangePasswordService } from './change-password/change-password.service';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { ChangePasswordComponent } from './change-password/change-password.component';
import { JwtModule, JWT_OPTIONS } from '@auth0/angular-jwt';
import { SessionStore } from '../store';



@NgModule({
  declarations: [DashboardComponent, ChangePasswordComponent],
  imports: [
    CommonModule,
    AdminRouteModule,
    SharedModule,
    ReactiveFormsModule,
    HttpClientModule

  ],
  providers: [ChangePasswordService],
  bootstrap: [DashboardComponent]
})
export class AdminModule { }
