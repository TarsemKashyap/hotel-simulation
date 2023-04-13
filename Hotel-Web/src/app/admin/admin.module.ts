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
import { InstructorComponent } from './instructor/instructor.component';
import { InstructorListComponent } from './instructor/instructor-list/instructor-list.component';
import { InstructorService } from './instructor/instructor.service';
import { AgGridModule } from 'ag-grid-angular';



@NgModule({
  declarations: [DashboardComponent, ChangePasswordComponent, InstructorComponent, InstructorListComponent],
  imports: [
    CommonModule,
    AdminRouteModule,
    SharedModule,
    ReactiveFormsModule,
    HttpClientModule,
    AgGridModule
  ],
  providers: [ChangePasswordService,InstructorService],
  bootstrap: [DashboardComponent]
})
export class AdminModule { }
