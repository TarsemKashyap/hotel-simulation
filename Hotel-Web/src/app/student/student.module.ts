import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StudentRouteModule } from './student-route.module';
import { ChangePasswordComponent } from '../admin';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { AgGridModule } from 'ag-grid-angular';
import { MaterialModule } from '../material.module';
import { SharedModule } from '../shared/shared.module';
import { StudentDashboard } from './dashboard/student-dashboard.component';
import { ChangePasswordService } from '../admin/change-password/change-password.service';
import { AccountService } from '../public/account';
import { DecisionComponent } from './decision/decision.component';
import { RoomComponent } from './room/room.component';

@NgModule({
  declarations: [StudentDashboard, DecisionComponent, RoomComponent],
  imports: [
    CommonModule,
    SharedModule,
    ReactiveFormsModule,
    HttpClientModule,
    AgGridModule,
    MaterialModule,
    StudentRouteModule
  ],
  providers: [ChangePasswordService,AccountService],
  bootstrap: [StudentDashboard],
})
export class StudentModule {}
