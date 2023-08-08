import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ReportRoutingModule } from './report-routing.module';
import { ObjectiveReportComponent } from './objective-report/objective-report.component';
import { ReportListComponent } from './report-list/report-list.component';
import { FormsModule } from '@angular/forms';
import { MaterialModule } from '../material.module';
import {ValueFormatterPipe} from 'src/app/shared/value-formatter.pipe';
import { PerformanceComponent } from './performance/performance.component';


@NgModule({
  declarations: [
    ObjectiveReportComponent,
    ReportListComponent,
    ValueFormatterPipe,
    PerformanceComponent
  ],
  imports: [
    CommonModule,
    ReportRoutingModule,
    FormsModule,
    MaterialModule
  ]
})
export class ReportModule { }
