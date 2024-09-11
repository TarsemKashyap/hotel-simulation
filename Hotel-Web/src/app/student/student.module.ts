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
import { DecisionComponent } from './decision/decision.component';
import { RoomComponent } from './room/room.component';
import { AttributeComponent } from './attribute/attribute.component';
import { PriceComponent } from './price/price.component';
import { MarketingComponent } from './marketing/marketing.component';
import { GoalSettingComponent } from './goal-setting/goal-setting.component';
import { LoanComponent } from './loan/loan.component';
import { NumberCommaSeparatorDirective } from './comma-seperator';

@NgModule({
  declarations: [
    StudentDashboard,
    DecisionComponent,
    RoomComponent,
    AttributeComponent,
    PriceComponent,
    MarketingComponent,
    GoalSettingComponent,
    LoanComponent,
    NumberCommaSeparatorDirective

  ],
  imports: [
    CommonModule,
    SharedModule,
    ReactiveFormsModule,
    HttpClientModule,
    AgGridModule,
    MaterialModule,
    StudentRouteModule,
    FormsModule,
  ],
  providers: [ChangePasswordService, AccountService],
  bootstrap: [StudentDashboard],
})
export class StudentModule {}
