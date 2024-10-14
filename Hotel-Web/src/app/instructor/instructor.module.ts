import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { AgGridModule } from 'ag-grid-angular';
import { MaterialModule } from '../material.module';
import { SharedModule } from '../shared/shared.module';
import { InstructorDashboard } from './dashboard/instructor-dashboard.component';
import { ChangePasswordService } from '../admin/change-password/change-password.service';
import { InstructorRouteModule } from './instructor-route.module';

@NgModule({
  declarations: [InstructorDashboard],
  imports: [
    CommonModule,
    SharedModule,
    ReactiveFormsModule,
    HttpClientModule,
    AgGridModule,
    MaterialModule,
    InstructorRouteModule,
  ],
  providers: [ChangePasswordService],
  bootstrap: [InstructorDashboard],
})
export class InstructorModule {}
