import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { AgGridModule } from 'ag-grid-angular';
import { MaterialModule } from 'src/app/material.module';
import { DecisionComponent } from './decision/decision.component';
import { AttributeComponent } from './attribute/attribute.component';
import { GoalSettingComponent } from './goal-setting/goal-setting.component';
import { LoanComponent } from './loan/loan.component';
import { MarketingComponent } from './marketing/marketing.component';
import { PriceComponent } from './price/price.component';
import { RoomComponent } from './room/room.component';
import { RouterModule } from '@angular/router';
import { StudentDecisionsComponent } from './student-decisions/student-decisions.component';

@NgModule({
  declarations: [
    DecisionComponent,
    AttributeComponent,
    GoalSettingComponent,
    LoanComponent,
    MarketingComponent,
    PriceComponent,
    RoomComponent,
    StudentDecisionsComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    HttpClientModule,
    AgGridModule,
    MaterialModule,
    FormsModule,
    RouterModule
  ],
  exports: [
    DecisionComponent,
    AttributeComponent,
    GoalSettingComponent,
    LoanComponent,
    MarketingComponent,
    PriceComponent,
    RoomComponent,
  ],
  bootstrap: [DecisionComponent],
})
export class DecisionModule {}
