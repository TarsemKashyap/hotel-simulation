import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DashboardComponent } from './dashboard/admin-dashboard.component';
import { AdminRouteModule } from './admin-route.module';
import { SharedModule } from '../shared/shared.module';
import { ChangePasswordService } from './change-password/change-password.service';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { ChangePasswordComponent } from './change-password/change-password.component';
import { InstructorComponent } from './instructor/instructor.component';
import { InstructorListComponent } from './instructor/instructor-list/instructor-list.component';
import { InstructorService } from './instructor/instructor.service';
import { AgGridModule } from 'ag-grid-angular';
import { InstructorEditComponent } from './instructor';
import { MaterialModule } from '../material.module';

@NgModule({
  declarations: [
    DashboardComponent,
    ChangePasswordComponent,
    InstructorComponent,
    InstructorListComponent,
    InstructorEditComponent,
  ],
  imports: [
    CommonModule,
    AdminRouteModule,
    SharedModule,
    ReactiveFormsModule,
    FormsModule,
    HttpClientModule,
    AgGridModule,
   MaterialModule
  ],
  providers: [ChangePasswordService, InstructorService],
  bootstrap: [DashboardComponent],
})
export class AdminModule {}
