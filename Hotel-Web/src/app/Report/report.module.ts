import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ReportRoutingModule } from './report-routing.module';
import { ObjectiveReportComponent } from './objective-report/objective-report.component';
import { ReportListComponent } from './report-list/report-list.component';
import { FormsModule } from '@angular/forms';
import { MaterialModule } from '../material.module';
import {ValueFormatterPipe} from 'src/app/shared/value-formatter.pipe';
import { PerformanceComponent } from './performance/performance.component';
import { IncomeComponent } from './income/income.component';
import { BalanceComponent } from './balance/balance.component';
import { CashflowComponent } from './cashflow/cashflow.component';
import { OccupancyComponent } from './occupancy/occupancy.component';
import { AvgDailyRateComponent } from './avg-daily-rate/avg-daily-rate.component';
import { RevParGoparComponent } from './rev-par-gopar/rev-par-gopar.component';
import { RoomRateComponent } from './room-rate/room-rate.component';
import { MarketShareRevenueComponent } from './market-share-revenue/market-share-revenue.component';
import { MarketShareRoomSoldComponent } from './market-share-roomsold/market-share-roomsold.component';
import { MarketSharePositionAloneComponent } from './market-share-position-alone/market-share-position-alone.component';
import { AttributeAmentitiesComponent } from './attribute-amentities/attribute-amentities.component';
import { MarketExpenditureComponent } from './market-expenditure/market-expenditure.component';
import { PositionMapComponent } from './position-map/position-map.component';
import { QualityRatingComponent } from './quality-rating/quality-rating.component';


@NgModule({
  declarations: [
    ObjectiveReportComponent,
    ReportListComponent,
    ValueFormatterPipe,
    PerformanceComponent,
    IncomeComponent,
    BalanceComponent,
    CashflowComponent,
    OccupancyComponent,
    AvgDailyRateComponent,
    RevParGoparComponent,
    RoomRateComponent,
    MarketShareRevenueComponent,
    MarketShareRoomSoldComponent,
    MarketSharePositionAloneComponent,
    AttributeAmentitiesComponent,
    MarketExpenditureComponent,
    PositionMapComponent,
    QualityRatingComponent
  ],
  imports: [
    CommonModule,
    ReportRoutingModule,
    FormsModule,
    MaterialModule
  ]
})
export class ReportModule { }
