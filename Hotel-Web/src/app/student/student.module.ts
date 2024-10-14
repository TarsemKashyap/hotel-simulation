import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StudentRouteModule } from './student-route.module';
import { ChangePasswordComponent } from '../admin';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { AgGridModule } from 'ag-grid-angular';
import { MaterialModule } from '../material.module';
import { SharedModule } from '../shared/shared.module';
import { StudentDashboard } from './dashboard/student-dashboard.component';
import { ChangePasswordService } from '../admin/change-password/change-password.service';
import { AccountService } from '../public/account';
import { DecisionModule } from '../shared/decisions';

@NgModule({
  declarations: [StudentDashboard],
  imports: [
    CommonModule,
    SharedModule,
    ReactiveFormsModule,
    HttpClientModule,
    AgGridModule,
    MaterialModule,
    StudentRouteModule,
    FormsModule,
    DecisionModule,
  ],
  providers: [ChangePasswordService, AccountService],
  bootstrap: [StudentDashboard],
})
export class StudentModule {}
