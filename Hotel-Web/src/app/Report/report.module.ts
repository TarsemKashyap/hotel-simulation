import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { FormsModule } from '@angular/forms';
import { MaterialModule } from '../material.module';
import {ValueFormatterPipe} from 'src/app/shared/value-formatter.pipe';
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
import { PerformanceInstComponent } from './performance-inst/performance-inst.component';
import { Fmt2Pipe, FmtPipe } from '../shared/FmtPipe';
import { SummeryAllHotelComponent } from './summery-all-hotel/summery-all-hotel.component';
import { ObjectiveReportComponent } from './objective-report/objective-report.component';
import { PerformanceComponent } from './performance/performance.component';
import { ReportListComponent } from './report-list/report-list.component';
import { ReportRoutingModule } from './report-routing.module';
import { DemandReportComponent } from './demand-report/demand-report.component';
import { ReportMenuComponent } from './report-menu/report-menu.component';
import { ClassModule } from '../shared/class/class.module';


@NgModule({
  declarations: [
    ObjectiveReportComponent,
    ReportListComponent,
    ValueFormatterPipe,
    FmtPipe,
    Fmt2Pipe,
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
    QualityRatingComponent,
    PerformanceInstComponent,
    SummeryAllHotelComponent,
    DemandReportComponent,
    ReportMenuComponent
  ],
  imports: [
    CommonModule,
    ReportRoutingModule,
    FormsModule,
    MaterialModule,
  ]
})
export class ReportModule { }
